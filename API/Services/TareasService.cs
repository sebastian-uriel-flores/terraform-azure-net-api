using Microsoft.EntityFrameworkCore;
using TerraAzSQLAPI.Models;

namespace TerraAzSQLAPI.Services;

public interface ITareasService
{
    IEnumerable<Tarea> Get();

    Task<Tarea?> Get(Guid id);

    Task<Tarea> Save(Tarea tarea);

    Task<bool> Update(Guid id, Tarea tarea);

    Task<bool> Delete(Guid id);
}
public class TareasService : ITareasService
{
    TareasContext context;

    public TareasService(TareasContext dbContext)
    {
        this.context = dbContext;
    }

    public IEnumerable<Tarea> Get()
    {
        return context.Tareas.Include(t => t.Categoria);
    }
    
    public async Task<Tarea?> Get(Guid id)
    {
        return await context.Tareas.Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.TareaID == id);
    }

    public async Task<Tarea> Save(Tarea tarea)
    {
        tarea.TareaID = Guid.NewGuid();
        tarea.FechaCreacion = DateTime.Now;        
        tarea.Categoria = null;
        await context.AddAsync(tarea);
        await context.SaveChangesAsync();

        return tarea;
    }

    public async Task<bool> Update(Guid id, Tarea tarea)
    {
        var tareaActual = context.Tareas.Find(id);

        if(tareaActual != null)
        {
            tareaActual.CategoriaID = tarea.CategoriaID;
            tareaActual.Titulo = tarea.Titulo;
            tareaActual.Descripcion = tarea.Descripcion;
            tareaActual.PrioridadTarea = tarea.PrioridadTarea;

            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(Guid id)
    {
        var tareaActual = context.Tareas.Find(id);

        if(tareaActual != null)
        {
            context.Remove(tareaActual);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}