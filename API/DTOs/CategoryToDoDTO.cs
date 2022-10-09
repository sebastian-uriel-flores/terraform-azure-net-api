using DemoAPIAzure.Entities;

namespace DemoAPIAzure.DTOs;

public class CategoryToDoDTO
{
    public Guid ToDoId { get; set; }    
    public string Title { get; set; }    
    public ToDoPriority Priority { get; set; }
}