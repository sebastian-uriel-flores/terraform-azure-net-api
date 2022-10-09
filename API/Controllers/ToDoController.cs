using Microsoft.AspNetCore.Mvc;
using DemoAPIAzure.Entities;
using DemoAPIAzure.Services;
using DemoAPIAzure.DTOs;

namespace DemoAPIAzure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    IToDoService toDoService;

    public ToDoController(IToDoService toDoService)
    {
        this.toDoService = toDoService;        
    }

    [HttpGet]
    public async Task<ActionResult<List<ToDoDTO>>> Get()
    {
        return await toDoService.Get();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ToDoDTO>> GetById(Guid id)
    {
        var toDo = await toDoService.Get(id);

        return toDo is null ? NotFound() : Ok(toDo);
    }
    
    [HttpPost]
    public async Task<ActionResult<ToDoDTO>> Save([FromBody] ToDo toDo)
    {        
        var createdResource = await toDoService.Save(toDo);

        var actionName = nameof(ToDoController.GetById);
        var controllerName = "toDo";
        var routeValues = new { id = createdResource.ToDoId };
        return CreatedAtAction(actionName, controllerName, routeValues, createdResource);
    }

    

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] ToDo toDo)
    {
        var found = await toDoService.Update(id, toDo);
        
        return found ? Ok() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var found = await toDoService.Delete(id);

        return found ? Ok() : NotFound();
    }
}