namespace DemoAPIAzure.DTOs;

public class CategoryDTO
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }    
    public string Description { get; set; }
    public int Weight { get; set; }

    public List<CategoryToDoDTO> ToDos { get; set; }
}