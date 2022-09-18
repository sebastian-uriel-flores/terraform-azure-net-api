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
    
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] Categoria categoria)
    {
        logger.LogInformation("CategoriaController.Save");
        await categoriaService.Save(categoria);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Categoria categoria)
    {
        logger.LogInformation("CategoriaController.Update");
        var found = await categoriaService.Update(id, categoria);

        return found ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        logger.LogInformation("CategoriaController.Delete");
        var found = await categoriaService.Delete(id);

        return found ? Ok() : NotFound();
    }
}