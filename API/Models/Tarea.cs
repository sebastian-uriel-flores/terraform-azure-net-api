using System.Text.Json.Serialization;

namespace TerraAzSQLAPI.Models;

public class Tarea
{
    public Guid TareaID { get; set; }

    public Guid CategoriaId { get; set; }

    public string Titulo { get; set; }
    
    public string? Descripcion { get; set; }

    public Prioridad PrioridadTarea { get; set; }

    public DateTime FechaCreacion { get; set; }
    
    public virtual Categoria? Categoria { get; set; }

    public string? Resumen { get; set; }
}

public enum Prioridad
{
    Baja,
    Media,
    Alta
}