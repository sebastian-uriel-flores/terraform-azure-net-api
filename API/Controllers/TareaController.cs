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
    
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] Tarea tarea)
    {
        logger.LogInformation("TareaController.Save");
        await tareasService.Save(tarea);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Tarea tarea)
    {
        logger.LogInformation("TareaController.Update");
        var found = await tareasService.Update(id, tarea);
        
        return found ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        logger.LogInformation("TareaController.Delete");
        var found = await tareasService.Delete(id);

        return found ? Ok() : NotFound();
    }
}