using Microsoft.AspNetCore.Mvc;
using DemoAPIAzure.Services;

namespace DemoAPIAzure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    ILogger<HealthController> logger;
    IHealthService healthService;

    ToDoContext dbContext;

    public HealthController(ILogger<HealthController> logger, IHealthService healthService, ToDoContext db)
    {
        this.logger = logger;
        this.healthService = healthService;
        this.dbContext = db;
    }

    [HttpGet]
    public IActionResult CheckHealth()
    {
        logger.LogInformation("HealthController.CheckHealth");
        return Ok(healthService.CheckHealth());
    }
    
    [HttpGet]
    [Route("CreateDb")]
    public IActionResult CreateDatabase()
    {
        logger.LogInformation("HealthController.CreateDatabase");
        dbContext.Database.EnsureCreated();
        return Ok();
    }
}