using Microsoft.EntityFrameworkCore;
using DemoAPIAzure.Models;

namespace DemoAPIAzure.Services;

public interface IJobService
{
    IEnumerable<Job> Get();

    Task<Job?> Get(Guid id);

    Task<Job> Save(Job job);

    Task<bool> Update(Guid id, Job job);

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

    public async Task<Job> Save(Job job)
    {
        job.JobId = Guid.NewGuid();
        job.CreationDate = DateTime.Now;        
        job.Category = null;
        await context.AddAsync(job);
        await context.SaveChangesAsync();

        return job;
    }

    public async Task<bool> Update(Guid id, Job job)
    {
        var jobActual = context.Jobs.Find(id);

        if(jobActual != null)
        {
            jobActual.CategoryId = job.CategoryId;
            jobActual.Title = job.Title;
            jobActual.Description = job.Description;
            jobActual.Priority = job.Priority;

            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(Guid id)
    {
        var jobActual = context.Jobs.Find(id);

        if(jobActual != null)
        {
            context.Remove(jobActual);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}