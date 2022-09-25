using Microsoft.EntityFrameworkCore;
using DemoAPIAzure.Models;

namespace DemoAPIAzure.Services;

public interface IJobService
{
    IEnumerable<Job> Get();

    Task<Job?> Get(Guid id);

    Task<Job> Save(Job tarea);

    Task<bool> Update(Guid id, Job tarea);

    Task<bool> Delete(Guid id);
}
public class JobService : IJobService
{
    JobContext context;

    public JobService(JobContext dbContext)
    {
        this.context = dbContext;
    }

    public IEnumerable<Job> Get()
    {
        return context.Jobs.Include(t => t.Category);
    }
    
    public async Task<Job?> Get(Guid id)
    {
        return await context.Jobs.Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.JobId == id);
    }

    public async Task<Job> Save(Job tarea)
    {
        tarea.JobId = Guid.NewGuid();
        tarea.CreationDate = DateTime.Now;        
        tarea.Category = null;
        await context.AddAsync(tarea);
        await context.SaveChangesAsync();

        return tarea;
    }

    public async Task<bool> Update(Guid id, Job tarea)
    {
        var tareaActual = context.Jobs.Find(id);

        if(tareaActual != null)
        {
            tareaActual.CategoryId = tarea.CategoryId;
            tareaActual.Title = tarea.Title;
            tareaActual.Description = tarea.Description;
            tareaActual.Priority = tarea.Priority;

            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(Guid id)
    {
        var tareaActual = context.Jobs.Find(id);

        if(tareaActual != null)
        {
            context.Remove(tareaActual);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}