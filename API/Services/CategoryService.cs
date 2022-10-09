using AutoMapper;
using AutoMapper.QueryableExtensions;
using DemoAPIAzure.DTOs;
using DemoAPIAzure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoAPIAzure.Services;

public interface ICategoryService
{
    Task<List<CategoryDTO>> Get();

    Task<CategoryDTO> Get(Guid id);

    Task<Guid> Save(Category category);

    Task<bool> Update(Guid id, Category category);

    Task<bool> Delete(Guid id);
}
public class CategoryService : ICategoryService
{
    private readonly ToDoContext context;
    private readonly IMapper mapper;

    public CategoryService(ToDoContext dbContext, IMapper mapper)
    {
        this.context = dbContext;
        this.mapper = mapper;
    }

    public async Task<List<CategoryDTO>> Get()
    {
        return await context.Categories            
            .ProjectTo<CategoryDTO>(mapper.ConfigurationProvider)
            .OrderByDescending(c => c.Weight)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<CategoryDTO> Get(Guid id)
    {
        var category = await context.Categories
            .Include(c => c.ToDos)            
            .FirstOrDefaultAsync(c => c.CategoryId == id);
        return mapper.Map<CategoryDTO>(category);
    }

    public async Task<Guid> Save(Category category)
    {
        category.CategoryId = Guid.NewGuid();
        category.ToDos = null;
        await context.AddAsync(category);
        await context.SaveChangesAsync();

        return category.CategoryId;
    }

    public async Task<bool> Update(Guid id, Category category)
    {
        var currentCategory = await context.Categories.FindAsync(id);

        if(currentCategory is null) return false;
        
        currentCategory.Name = category.Name;        
        currentCategory.Description = category.Description;
        currentCategory.Weight = category.Weight;

        context.Update(currentCategory);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var currentCategory = await context.Categories.FindAsync(id);

        if(currentCategory is null) return false;

        context.Remove(currentCategory);
        await context.SaveChangesAsync();
        return true;
    }
}