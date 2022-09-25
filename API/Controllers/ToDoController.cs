using Microsoft.AspNetCore.Mvc;
using DemoAPIAzure.Models;
using DemoAPIAzure.Services;

namespace DemoAPIAzure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    ILogger<CategoryController> logger;
    IToDoService toDoService;

    public ToDoController(ILogger<CategoryController> logger, IToDoService toDoService)
    {
        this.logger = logger;
        this.toDoService = toDoService;        
    }

    [HttpGet]
    public IActionResult Get()
    {
        logger.LogInformation(nameof(ToDoController) + "." +nameof(ToDoController.Get) + "-"+this.GetType().Name);
        return Ok(toDoService.Get());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        logger.LogInformation(nameof(ToDoController) + "." +nameof(ToDoController.GetById) + "-"+this.GetType().Name);
        var toDo = await toDoService.Get(id);

        return toDo != null ? Ok(toDo) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ToDo toDo)
    {        
        logger.LogInformation(nameof(ToDoController) + "." +nameof(ToDoController.Save) + "-"+this.GetType().Name);
        var createdResource = await toDoService.Save(toDo);

        var actionName = nameof(ToDoController.GetById);
        var controllerName = "toDo";
        var routeValues = new { id = createdResource.ToDoId };
        return CreatedAtAction(actionName, controllerName, routeValues, createdResource);
    }

    

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ToDo toDo)
    {
        logger.LogInformation(nameof(ToDoController) + "." +nameof(ToDoController.Update) + "-"+this.GetType().Name);
        var found = await toDoService.Update(id, toDo);
        
        return found ? Ok() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        logger.LogInformation(nameof(ToDoController) + "." +nameof(ToDoController.Delete) + "-"+this.GetType().Name);
        var found = await toDoService.Delete(id);

        return found ? Ok() : NotFound();
    }
}