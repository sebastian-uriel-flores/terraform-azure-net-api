namespace DemoAPIAzure.Entities;

public class Category
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }    
    public string Description { get; set; }
    public int Weight { get; set; }

    public List<ToDo> ToDos { get; set; }
}