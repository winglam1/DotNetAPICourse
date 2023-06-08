using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSalaryEFController : ControllerBase
{
    DataContextEF _entityFramework;
    IMapper _mapper;

    public UserSalaryEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);
        _mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.CreateMap<UserToAddDTO, UserSalary>();
        }));
    }

    [HttpGet("GetUserSalary/{userId}")]
    public IEnumerable<UserSalary> GetUserSalary(int userId)
    {
        return _entityFramework.UserSalary
            .Where(u => u.UserId == userId)
            .ToList();
    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUser(int userId)
    {
        UserSalary? userSalary = _entityFramework.UserSalary
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserSalary>();

        if (userSalary != null)
            return userSalary;

        throw new Exception("Failed to get user salary.");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        UserSalary? userSalaryDb = _entityFramework.UserSalary
            .Where(u => u.UserId == userSalary.UserId)
            .FirstOrDefault<UserSalary>();

        if (userSalaryDb != null)
        {
            userSalaryDb.Salary = userSalary.Salary;
            userSalaryDb.AvgSalary = userSalary.AvgSalary;

            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to update user salary");
        }

        throw new Exception("Failed to get user salary to update");
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalaryToAddDTO userSalary)
    {
        UserSalary userSalaryDb = _mapper.Map<UserSalary>(userSalary);

        userSalaryDb.Salary = userSalary.Salary;
        userSalaryDb.AvgSalary = userSalary.AvgSalary;

        _entityFramework.Add(userSalaryDb);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }

        throw new Exception("Failed to add user salary");
    }
    
    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary? userSalaryDb = _entityFramework.UserSalary
            .Where(u => u.UserId == userId)
            .FirstOrDefault();

        if (userSalaryDb != null)
        {
            _entityFramework.UserSalary.Remove(userSalaryDb);

            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to delete user salary");
        }

        throw new Exception("Failed to get user salary to delete");
    }
}