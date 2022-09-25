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
        var job = await jobService.Get(id);

        return job != null ? Ok(job) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] Job job)
    {        
        logger.LogInformation(nameof(JobController) + "." +nameof(JobController.Save) + "-"+this.GetType().Name);
        var createdResource = await jobService.Save(job);

        var actionName = nameof(JobController.GetById);
        var controllerName = "job";
        var routeValues = new { id = createdResource.JobId };
        return CreatedAtAction(actionName, controllerName, routeValues, createdResource);
    }

    

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Job job)
    {
        logger.LogInformation(nameof(JobController) + "." +nameof(JobController.Update) + "-"+this.GetType().Name);
        var found = await jobService.Update(id, job);
        
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