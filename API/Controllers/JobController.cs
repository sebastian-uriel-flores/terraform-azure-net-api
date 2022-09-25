using Microsoft.AspNetCore.Mvc;
using DemoAPIAzure.Models;
using DemoAPIAzure.Services;

namespace DemoAPIAzure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    ILogger<CategoryController> logger;
    IJobService jobService;

    public JobController(ILogger<CategoryController> logger, IJobService jobService)
    {
        this.logger = logger;
        this.jobService = jobService;        
    }

    [HttpGet]
    public IActionResult Get()
    {
        logger.LogInformation(nameof(JobController) + "." +nameof(JobController.Get) + "-"+this.GetType().Name);
        return Ok(jobService.Get());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        logger.LogInformation(nameof(JobController) + "." +nameof(JobController.GetById) + "-"+this.GetType().Name);
        var tarea = await jobService.Get(id);

        return tarea != null ? Ok(tarea) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] Job tarea)
    {        
        logger.LogInformation(nameof(JobController) + "." +nameof(JobController.Save) + "-"+this.GetType().Name);
        var createdResource = await jobService.Save(tarea);

        var actionName = nameof(JobController.GetById);
        var controllerName = "tarea";
        var routeValues = new { id = createdResource.JobId };
        return CreatedAtAction(actionName, controllerName, routeValues, createdResource);
    }

    

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Job tarea)
    {
        logger.LogInformation(nameof(JobController) + "." +nameof(JobController.Update) + "-"+this.GetType().Name);
        var found = await jobService.Update(id, tarea);
        
        return found ? Ok() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        logger.LogInformation(nameof(JobController) + "." +nameof(JobController.Delete) + "-"+this.GetType().Name);
        var found = await jobService.Delete(id);

        return found ? Ok() : NotFound();
    }
}