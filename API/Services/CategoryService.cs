using DemoAPIAzure.Models;

namespace DemoAPIAzure.Services;

public interface ICategoryService
{
    IEnumerable<Category> Get();

    Task<Category?> Get(Guid id);

    Task<Category> Save(Category category);

    Task<bool> Update(Guid id, Category category);

    Task<bool> Delete(Guid id);
}
public class CategoryService : ICategoryService
{
    ToDoContext context;

    public CategoryService(ToDoContext dbContext)
    {
        this.context = dbContext;
    }

    public IEnumerable<Category> Get()
    {
        return context.Categories;
    }

    public async Task<Category?> Get(Guid id)
    {
        return await context.Categories.FindAsync(id);
    }

    public async Task<Category> Save(Category category)
    {
        category.CategoryId = Guid.NewGuid();
        category.ToDos = null;
        await context.AddAsync(category);
        await context.SaveChangesAsync();

        return category;
    }

    public async Task<bool> Update(Guid id, Category category)
    {
        var currentCategory = context.Categories.Find(id);

        if(currentCategory != null)
        {
            currentCategory.Name = category.Name;
            currentCategory.Description = category.Description;
            currentCategory.Weight = category.Weight;

            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(Guid id)
    {
        var currentCategory = context.Categories.Find(id);

        if(currentCategory != null)
        {
            context.Remove(currentCategory);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}