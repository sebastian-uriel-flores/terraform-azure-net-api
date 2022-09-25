using Microsoft.AspNetCore.Mvc;
using DemoAPIAzure.Models;
using DemoAPIAzure.Services;

namespace DemoAPIAzure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    ILogger<CategoryController> logger;
    ICategoryService categoryService;

    public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
    {
        this.logger = logger;
        this.categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        logger.LogInformation("CategoryController.Get");
        return Ok(categoryService.Get());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        logger.LogInformation("CategoryController.GetById");        
        var categoria = await categoryService.Get(id);
        
        return categoria != null ? Ok(categoria) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] Category categoria)
    {
        logger.LogInformation("CategoryController.Save");
        var createdResource = await categoryService.Save(categoria);
        var actionName = nameof(CategoryController.GetById);
        var controllerName = "categoria";
        var routeValues = new { id = createdResource.CategoryId };
        return CreatedAtAction(actionName, controllerName, routeValues, createdResource);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Category categoria)
    {
        logger.LogInformation("CategoryController.Update");
        var found = await categoryService.Update(id, categoria);

        return found ? Ok() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        logger.LogInformation("CategoryController.Delete");
        var found = await categoryService.Delete(id);

        return found ? Ok() : NotFound();
    }
}