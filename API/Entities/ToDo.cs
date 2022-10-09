namespace DemoAPIAzure.Entities;

public class ToDo
{
    public Guid ToDoId { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; }    
    public string Description { get; set; }
    public ToDoPriority Priority { get; set; }
    public DateTime CreationDate { get; set; }    
    public Category Category { get; set; }
}

public enum ToDoPriority
{
    Low = 1,
    Middle = 2,
    High = 3
}