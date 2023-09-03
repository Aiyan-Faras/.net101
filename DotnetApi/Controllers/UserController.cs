using Microsoft.AspNetCore.Mvc;

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


    [HttpGet("Testing")]
    public DateTime GetDateTime() {
        return _dapper.LoadDAtaSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet ("Users")]
    public String[] Get()
    {

        return new String[] { "user1", "user2" };
        // return Enumerable.Range(1, 5).Select(index => new UserController
        // {
        //     Date = DateTime.Now.AddDays(index),
        //     TemperatureC = Random.Shared.Next(-20, 55),
        //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        // })
        // .ToArray();
    }
}
