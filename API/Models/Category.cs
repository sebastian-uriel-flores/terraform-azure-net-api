using System.Text.Json.Serialization;

namespace DemoAPIAzure.Models;

public class Category
{
    public Guid CategoryId { get; set; }

    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public int Weight { get; set; }

    [JsonIgnore]
    public virtual ICollection<Job>? Jobs { get; set; }
}