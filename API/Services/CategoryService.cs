using DemoAPIAzure.Models;

namespace DemoAPIAzure.Services;

public interface ICategoryService
{
    IEnumerable<Category> Get();

    Task<Category?> Get(Guid id);

    Task<Category> Save(Category categoria);

    Task<bool> Update(Guid id, Category categoria);

    Task<bool> Delete(Guid id);
}
public class CategoryService : ICategoryService
{
    JobContext context;

    public CategoryService(JobContext dbContext)
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

    public async Task<Category> Save(Category categoria)
    {
        categoria.CategoryId = Guid.NewGuid();
        categoria.Jobs = null;
        await context.AddAsync(categoria);
        await context.SaveChangesAsync();

        return categoria;
    }

    public async Task<bool> Update(Guid id, Category categoria)
    {
        var categoriaActual = context.Categories.Find(id);

        if(categoriaActual != null)
        {
            categoriaActual.Name = categoria.Name;
            categoriaActual.Description = categoria.Description;
            categoriaActual.Weight = categoria.Weight;

            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(Guid id)
    {
        var categoriaActual = context.Categories.Find(id);

        if(categoriaActual != null)
        {
            context.Remove(categoriaActual);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}