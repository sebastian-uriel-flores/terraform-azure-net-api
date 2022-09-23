using Microsoft.AspNetCore.Mvc;
using TerraAzSQLAPI.Models;
using TerraAzSQLAPI.Services;

namespace TerraAzSQLAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    ILogger<CategoriaController> logger;
    ICategoriaService categoriaService;

    public CategoriaController(ILogger<CategoriaController> logger, ICategoriaService categoriaService)
    {
        this.logger = logger;
        this.categoriaService = categoriaService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        logger.LogInformation("CategoriaController.Get");
        return Ok(categoriaService.Get());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        logger.LogInformation("CategoriaController.GetById");        
        var categoria = await categoriaService.Get(id);
        
        return categoria != null ? Ok(categoria) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] Categoria categoria)
    {
        logger.LogInformation("CategoriaController.Save");
        var createdResource = await categoriaService.Save(categoria);
        var actionName = nameof(CategoriaController.GetById);
        var controllerName = "categoria";
        var routeValues = new { id = createdResource.CategoriaID };
        return CreatedAtAction(actionName, controllerName, routeValues, createdResource);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Categoria categoria)
    {
        logger.LogInformation("CategoriaController.Update");
        var found = await categoriaService.Update(id, categoria);

        return found ? Ok() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        logger.LogInformation("CategoriaController.Delete");
        var found = await categoriaService.Delete(id);

        return found ? Ok() : NotFound();
    }
}