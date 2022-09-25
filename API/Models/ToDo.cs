namespace DemoAPIAzure.Models;

public class ToDo
{
    public Guid ToDoId { get; set; }

    public Guid CategoryId { get; set; }

    public string? Title { get; set; }
    
    public string? Description { get; set; }

    public ToDoPriority Priority { get; set; }

    public DateTime CreationDate { get; set; }
    
    public virtual Category? Category { get; set; }
}

public enum ToDoPriority
{
    Low,
    Middle,
    High
}