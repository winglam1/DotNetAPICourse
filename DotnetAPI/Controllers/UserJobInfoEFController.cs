using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoEFController : ControllerBase
{
    DataContextEF _entityFramework;
    IMapper _mapper;

    public UserJobInfoEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);
        _mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.CreateMap<UserToAddDTO, UserJobInfo>();
        }));
    }

    [HttpGet("GetUserJobInfo")]
    public IEnumerable<UserJobInfo> GetUserJobInfo()
    {
        IEnumerable<UserJobInfo> userJobInfo = _entityFramework.UserJobInfo.ToList<UserJobInfo>();

        return userJobInfo;
    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {
        UserJobInfo? userJobInfo = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserJobInfo>();

        if (userJobInfo != null)
            return userJobInfo;

        throw new Exception("Failed to get user job info.");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userJobInfo.UserId)
            .FirstOrDefault<UserJobInfo>();

        if (userJobInfoDb != null)
        {
            userJobInfoDb.Department = userJobInfo.Department;
            userJobInfoDb.JobTitle = userJobInfo.JobTitle;

            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to update user job info");
        }

        throw new Exception("Failed to get user job info to update");
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfoToAddDTO userJobInfo)
    {
        UserJobInfo userJobInfoDb = _mapper.Map<UserJobInfo>(userJobInfo);

        userJobInfoDb.Department = userJobInfo.Department;
        userJobInfoDb.JobTitle = userJobInfo.JobTitle;

        _entityFramework.Add(userJobInfoDb);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }

        throw new Exception("Failed to add user job info");
    }
    
    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserJobInfo>();

        if (userJobInfoDb != null)
        {
            _entityFramework.UserJobInfo.Remove(userJobInfoDb);

            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to delete user job info");
        }

        throw new Exception("Failed to get user job info to delete");
    }
}