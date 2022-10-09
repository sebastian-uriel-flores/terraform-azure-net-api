using Microsoft.EntityFrameworkCore;
using DemoAPIAzure.Entities;
using AutoMapper;
using DemoAPIAzure.DTOs;
using AutoMapper.QueryableExtensions;

namespace DemoAPIAzure.Services;

public interface IToDoService
{
    Task<List<ToDoDTO>> Get();

    Task<ToDoDTO> Get(Guid id);

    Task<Guid> Save(ToDo toDo);

    Task<bool> Update(Guid id, ToDo toDo);

    Task<bool> Delete(Guid id);
}
public class ToDoService : IToDoService
{
    private readonly ToDoContext context;
    private readonly IMapper mapper;

    public ToDoService(ToDoContext dbContext, IMapper mapper)
    {
        this.context = dbContext;
        this.mapper =  mapper;
    }

    public async Task<List<ToDoDTO>> Get()
    {
        return await context.ToDos
            .ProjectTo<ToDoDTO>(mapper.ConfigurationProvider)
            .OrderByDescending(td => td.Priority)
            .OrderBy(td => td.Title)
            .ToListAsync();
    }
    
    public async Task<ToDoDTO> Get(Guid id)
    {
        var toDo = await context.ToDos
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.ToDoId == id);
        
        return mapper.Map<ToDoDTO>(toDo);
    }

    public async Task<Guid> Save(ToDo toDo)
    {
        toDo.ToDoId = Guid.NewGuid();        
        toDo.CreationDate = DateTime.Now;        
        toDo.Category = null;
        await context.AddAsync(toDo);
        await context.SaveChangesAsync();

        return toDo.ToDoId;        
    }

    public async Task<bool> Update(Guid id, ToDo toDo)
    {
        var currentToDo = await context.ToDos.FindAsync(id);

        if(currentToDo is null) return false;

        currentToDo.CategoryId = toDo.CategoryId;
        currentToDo.Title = toDo.Title;
        currentToDo.Description = toDo.Description;
        currentToDo.Priority = toDo.Priority;

        context.Update(currentToDo);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var currentToDo = await context.ToDos.FindAsync(id);

        if(currentToDo is null) return false;

        context.Remove(currentToDo);
        await context.SaveChangesAsync();
        return true;
    }
}