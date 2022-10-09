using DemoAPIAzure.Entities;

namespace DemoAPIAzure.DTOs;

public class ToDoDTO
{
    public Guid ToDoId { get; set; }    
    public string Title { get; set; }    
    public string Description { get; set; }
    public ToDoPriority Priority { get; set; }
    public DateTime CreationDate { get; set; } 

    public ToDoCategoryDTO Category { get; set; }
}