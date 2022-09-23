using Microsoft.AspNetCore.Mvc;
using TerraAzSQLAPI.Models;
using TerraAzSQLAPI.Services;

namespace TerraAzSQLAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareaController : ControllerBase
{
    ILogger<CategoriaController> logger;
    ITareasService tareasService;

    public TareaController(ILogger<CategoriaController> logger, ITareasService tareasService)
    {
        this.logger = logger;
        this.tareasService = tareasService;        
    }

    [HttpGet]
    public IActionResult Get()
    {
        logger.LogInformation("TareaController.Get");
        return Ok(tareasService.Get());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        logger.LogInformation("TareaController.GetById");
        var tarea = await tareasService.Get(id);

        return tarea != null ? Ok(tarea) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] Tarea tarea)
    {
        logger.LogInformation("TareaController.Save");
        
        logger.LogInformation(nameof(TareaController) + "." +nameof(TareaController.Save) + "-"+this.GetType().Name);
        var createdResource = await tareasService.Save(tarea);

        var actionName = nameof(TareaController.GetById);
        var controllerName = "tarea";
        var routeValues = new { id = createdResource.TareaID };
        return CreatedAtAction(actionName, controllerName, routeValues, createdResource);

        /*var routeValues = new
        {
            action = nameof(TareaController.Get),
            controller = "tarea",
            id = createdResource.TareaID
        };
        return CreatedAtRoute(routeValues, createdResource);*/
        // Location: .../api/ValuesV2/1?version=1.0

        //return Ok();
    }

    

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Tarea tarea)
    {
        logger.LogInformation("TareaController.Update");
        var found = await tareasService.Update(id, tarea);
        
        return found ? Ok() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        logger.LogInformation("TareaController.Delete");
        var found = await tareasService.Delete(id);

        return found ? Ok() : NotFound();
    }
}