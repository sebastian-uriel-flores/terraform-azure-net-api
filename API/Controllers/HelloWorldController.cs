using Microsoft.AspNetCore.Mvc;
using TerraAzSQLAPI.Services;

namespace TerraAzSQLAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloWorldController : ControllerBase
{
    ILogger<HelloWorldController> logger;
    IHelloWorldService helloWorldService;

    TareasContext dbContext;

    public HelloWorldController(ILogger<HelloWorldController> logger, IHelloWorldService helloWorldService, TareasContext db)
    {
        this.logger = logger;
        this.helloWorldService = helloWorldService;
        this.dbContext = db;
    }

    [HttpGet]
    public IActionResult Get()
    {
        logger.LogInformation("HelloWorldController.Get");
        return Ok(helloWorldService.GetHelloWorld());
    }
    
    [HttpGet]
    [Route("CreateDb")]
    public IActionResult CreateDatabase()
    {
        logger.LogInformation("HelloWorldController.CreateDatabase");
        dbContext.Database.EnsureCreated();
        return Ok();
    }
}