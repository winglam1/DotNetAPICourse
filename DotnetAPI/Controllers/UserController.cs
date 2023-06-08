using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;

    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] FROM TutorialAppSchema.Users";

        IEnumerable<User> users = _dapper.LoadData<User>(sql);

        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] FROM TutorialAppSchema.Users
            WHERE UserId = " + userId.ToString();

        User user = _dapper.LoadDataSingle<User>(sql);

        return user;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
            UPDATE TutorialAppSchema.Users
            SET     
                [FirstName] = '" + user.FirstName + 
                "',[LastName] = '" + user.LastName +
                "',[Email] = '" + user.Email +
                "',[Gender] = '" + user.Gender +
                "',[Active] = '" + user.Active +
            "' WHERE UserId =" + user.UserId.ToString();

        Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql))
            return Ok();

        throw new Exception("Failed to update user");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDTO user)
    {
        string sql = @"INSERT INTO TutorialAppSchema.Users(
                            [FirstName],
                            [LastName],
                            [Email],
                            [Gender],
                            [Active]
                        ) VALUES (" +
                            "'" + user.FirstName +  
                            "','" + user.LastName +
                            "','" + user.Email +
                            "','" + user.Gender +
                            "','" + user.Active +                       
                            "')";

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
            return Ok();

        throw new Exception("Failed to add user");
    }
    
    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
            DELETE FROM [DotNetCourseDatabase].[TutorialAppSchema].[Users] 
                WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
            return Ok();

        throw new Exception("Failed to delete user");
    }

    [HttpGet("UserSalary/{userId}")]
    public IEnumerable<UserSalary> GetUserSalary(int userId)
    {
        return _dapper.LoadData<UserSalary>(@"
            SELECT 
                UserSalary.UserId, UserSalary.Salary, UserSalary.AverageSalary
            FROM 
                TutorialAppSchema.UserSalary
            WHERE UserId = " + userId.ToString());
    }

    [HttpPost("AddUserSalary")]
    public IActionResult PostUserSalary(UserSalary userSalaryForInsert)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.UserSalary (
                UserId,
                Salary
            ) VALUES (" + userSalaryForInsert.UserId.ToString() 
                + ", " + userSalaryForInsert.Salary
                + ")";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userSalaryForInsert);
        }

        throw new Exception("Adding UserSalary failed on save");
    }

    [HttpPut("UserSalary")]
    public IActionResult PutUserSalary(UserSalary userSalaryForUpdate)
    {
        string sql = "UPDATE TutorialAppSchema.UserSalary SET Salary="
                + userSalaryForUpdate.Salary
                + " WHERE UserId=" + userSalaryForUpdate.UserId.ToString();

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userSalaryForUpdate);
        }

        throw new Exception("Updating UserSalary failed on save");
    }

    [HttpDelete("UserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = "DELETE FROM TutorialAppSchema.UserSalary WHERE UserId=" + userId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Deleting UserSalary failed on save");
    }

    [HttpGet("UserJobInfo/{userId}")]
    public IEnumerable<UserJobInfo> GetUserJobInfo(int userId)
    {
        return _dapper.LoadData<UserJobInfo>(@"
            SELECT 
                UserJobInfo.UserId, UserJobInfo.JobTitle, UserJobInfo.Department
            FROM 
                TutorialAppSchema.UserJobInfo
            WHERE 
                UserId = " + userId.ToString());
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult PostUserJobInfo(UserJobInfo UserJobInfoForInsert)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.UserJobInfo (
                UserId,
                Department,
                JobTitle
            ) VALUES (" + UserJobInfoForInsert.UserId
                + ", '" + UserJobInfoForInsert.Department
                + "', '" + UserJobInfoForInsert.JobTitle
                + "')";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(UserJobInfoForInsert);
        }

        throw new Exception("Adding UserJobInfo failed on save");
    }

    [HttpDelete("UserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        string sql = "DELETE FROM TutorialAppSchema.UserJobInfo WHERE UserId=" + userId.ToString();

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Deleting UserJobInfo failed on save");
    }
}