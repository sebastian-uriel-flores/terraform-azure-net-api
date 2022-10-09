using Microsoft.AspNetCore.Mvc;
using DemoAPIAzure.Entities;
using DemoAPIAzure.Services;
using DemoAPIAzure.DTOs;

namespace DemoAPIAzure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    ICategoryService categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet]
    public async Task<List<CategoryDTO>> Get()
    {
        return await categoryService.Get();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryDTO>> GetById(Guid id)
    {
        var category = await categoryService.Get(id);
        
        return category is null ? NotFound() : Ok(category);
    }
    
    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> Save([FromBody] Category category)
    {
        var createdResource = await categoryService.Save(category);
        var actionName = nameof(CategoryController.GetById);
        var controllerName = "category";
        var routeValues = new { id = createdResource.CategoryId };
        return CreatedAtAction(actionName, controllerName, routeValues, createdResource);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] Category category)
    {
        var found = await categoryService.Update(id, category);

        return found ? Ok() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var found = await categoryService.Delete(id);

        return found ? Ok() : NotFound();
    }
}