using TerraAzSQLAPI.Models;

namespace TerraAzSQLAPI.Services;

public interface ICategoriaService
{
    IEnumerable<Categoria> Get();

    Task<Categoria?> Get(Guid id);

    Task<Categoria> Save(Categoria categoria);

    Task<bool> Update(Guid id, Categoria categoria);

    Task<bool> Delete(Guid id);
}
public class CategoriaService : ICategoriaService
{
    TareasContext context;

    public CategoriaService(TareasContext dbContext)
    {
        this.context = dbContext;
    }

    public IEnumerable<Categoria> Get()
    {
        return context.Categorias;
    }

    public async Task<Categoria?> Get(Guid id)
    {
        return await context.Categorias.FindAsync(id);
    }

    public async Task<Categoria> Save(Categoria categoria)
    {
        categoria.CategoriaID = Guid.NewGuid();
        categoria.Tareas = null;
        await context.AddAsync(categoria);
        await context.SaveChangesAsync();

        return categoria;
    }

    public async Task<bool> Update(Guid id, Categoria categoria)
    {
        var categoriaActual = context.Categorias.Find(id);

        if(categoriaActual != null)
        {
            categoriaActual.Nombre = categoria.Nombre;
            categoriaActual.Descripcion = categoria.Descripcion;
            categoriaActual.Peso = categoria.Peso;

            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(Guid id)
    {
        var categoriaActual = context.Categorias.Find(id);

        if(categoriaActual != null)
        {
            context.Remove(categoriaActual);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}