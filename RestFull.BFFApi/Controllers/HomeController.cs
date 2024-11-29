using Microsoft.AspNetCore.Mvc;

namespace RestFull.BFFApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("HelloWorld")]
    public IActionResult HelloWorld()
    {
        return Ok("Hello World");
    }
}
