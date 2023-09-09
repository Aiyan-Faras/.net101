using Microsoft.AspNetCore.Mvc;
using DotnetApi.Models;
using DotnetApi.Dtos;
using DotnetApi.Data;
using AutoMapper;


namespace DotnetApi.Controllers;

[ApiController]
[Route("[controller]")]


public class UserEFController : ControllerBase
{
    //DataContextEF _entityframework; Not gonna use because IUserRepository has DataContextEF object already
    IUserRepository _userRepository;
    IMapper _mapper;

    public UserEFController(IConfiguration _config, IUserRepository userRepository)
    {
        //_entityframework = new DataContextEF(_config);
        _userRepository = userRepository;
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.CreateMap<UserToAddDto, User>(); }));
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> result = _userRepository.GetUsers(); // _entityframework.Users.ToList<User>();
        return result;
    }

    [HttpGet("GetSingleUser/{UserId}")]

    public User GetUser(int UserId)
    {
        User? user = _userRepository.GetUser(UserId);  //_entityframework.Users.Where(u => u.UserId == UserId).FirstOrDefault<User>();
        if (user != null)
        {
            return user;
        }
        throw new Exception("User could not be found");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _userRepository.GetUser(user.UserId);  //_entityframework.Users.Where(u => u.UserId == user.UserId).FirstOrDefault<User>();

        if (user != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Update User");
        }
        throw new Exception("Failed to get User");
    }


    [HttpPost("AddUser")]

    public IActionResult AddUser(UserToAddDto userToAdd)
    {
        User userDb = _mapper.Map<User>(userToAdd);
        //_entityframework.Add(userDb);
        _userRepository.AddEntity<User>(userDb);


        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");
    }


}