using Microsoft.AspNetCore.Mvc;
using  DotnetApi.Data;
using  DotnetApi.Models;


namespace DotnetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }


    [HttpGet("GetDateTime")]
    public DateTime GetDateTime() {
        return _dapper.LoadDAtaSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        String Sql = @"Select * from TutorialAppSchema.Users";
        IEnumerable<User> result = _dapper.LoadDAta<User>(Sql);
        return result;
    }
    [HttpGet("GetUser/{UserId}")]
    public User Getuser(int UserId)
    {
        String Sql = @"Select * from TutorialAppSchema.Users where UserId =" + UserId.ToString();
        return _dapper.LoadDAtaSingle<User>(Sql);
    }

    [HttpPut("EditUser")]
    public IActionResult Edituser(User user) { 
            String Sql  = @"Update TutorialAppSchema.Users  
                SET [FirstName] = '"+user.FirstName+ @"',
                [LastName] = '"+user.LastName+ @"',
                [Email] = '"+user.Email+ @"',
                [Gender] = '"+user.Gender+ @"',
                [Active] = '"+user.Active+ @" '
            WHERE
                UserId = '"+user.UserId+ @"'";

        if (_dapper.ExecuteSql(Sql)) {
            return Ok();
        }
        throw new Exception("Could not Update the user");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(User user) {
        String Sql = @"INSERT INTO TutorialAppSchema.Users (
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active]
            )VALUES(
                  '" + user.FirstName + @"',
                 '" + user.LastName + @"',
                 '" + user.Email + @"',
                 '" + user.Gender + @"',
                 '" + user.Active + @"'
            )";

        if (_dapper.ExecuteSql(Sql)) {
            return Ok();
        }
        throw new Exception("Could not Add the user");
    }
}

