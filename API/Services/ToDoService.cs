using Microsoft.EntityFrameworkCore;
using DemoAPIAzure.Models;

namespace DemoAPIAzure.Services;

public interface IToDoService
{
    IEnumerable<ToDo> Get();

    Task<ToDo?> Get(Guid id);

    Task<ToDo> Save(ToDo toDo);

    Task<bool> Update(Guid id, ToDo toDo);

    Task<bool> Delete(Guid id);
}
public class ToDoService : IToDoService
{
    ToDoContext context;

    public ToDoService(ToDoContext dbContext)
    {
        this.context = dbContext;
    }

    public IEnumerable<ToDo> Get()
    {
        return context.ToDos.Include(t => t.Category);
    }
    
    public async Task<ToDo?> Get(Guid id)
    {
        return await context.ToDos.Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.ToDoId == id);
    }

    public async Task<ToDo> Save(ToDo toDo)
    {
        toDo.ToDoId = Guid.NewGuid();
        toDo.CreationDate = DateTime.Now;        
        toDo.Category = null;
        await context.AddAsync(toDo);
        await context.SaveChangesAsync();

        return toDo;
    }

    public async Task<bool> Update(Guid id, ToDo toDo)
    {
        var currentToDo = context.ToDos.Find(id);

        if(currentToDo != null)
        {
            currentToDo.CategoryId = toDo.CategoryId;
            currentToDo.Title = toDo.Title;
            currentToDo.Description = toDo.Description;
            currentToDo.Priority = toDo.Priority;

            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(Guid id)
    {
        var currentToDo = context.ToDos.Find(id);

        if(currentToDo != null)
        {
            context.Remove(currentToDo);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}