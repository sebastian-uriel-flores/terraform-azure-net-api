namespace DemoAPIAzure.Models;

public class Job
{
    public Guid JobId { get; set; }

    public Guid CategoryId { get; set; }

    public string? Title { get; set; }
    
    public string? Description { get; set; }

    public JobPriority Priority { get; set; }

    public DateTime CreationDate { get; set; }
    
    public virtual Category? Category { get; set; }
}

public enum JobPriority
{
    Low,
    Middle,
    High
}