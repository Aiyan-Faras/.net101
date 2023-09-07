using Microsoft.AspNetCore.Mvc;
using DotnetApi.Models;
//using DotnetApi.Dtos;
using DotnetApi.Data;
using AutoMapper;


namespace DotnetApi.Controllers;

[ApiController]
[Route("[controller]")]


public class UserEFController : ControllerBase
{
    DataContextEF _entityframework;

   // IMapper mapper;

    public UserEFController(IConfiguration _config)
    {
        _entityframework = new DataContextEF(_config);

        //_mapper = new Mapper(new MapperConfig(cfg => { cfg.CreateMap<UserToAddDto, User>(); }));
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> result = _entityframework.Users.ToList<User>();
        return result;
    }

    [HttpGet("GetSingleUser/{UserId}")]

    public User GetUser(int UserId)
    {
        User? user = _entityframework.Users.Where(u => u.UserId == UserId).FirstOrDefault<User>();
        if (user != null)
        {
            return user;
        }
        throw new Exception("User could not be found");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _entityframework.Users.Where(u => u.UserId == user.UserId).FirstOrDefault<User>();

        if (user != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;

            if (_entityframework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to Update User");
        }
        throw new Exception("Failed to get User");
    }


    [HttpPost("AddUser")]

    public IActionResult AddUser(User user)
    {
        User userDb = user; //_mapper.Map<User>(user);
        _entityframework.Add(userDb);
        if (_entityframework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");
    }


}