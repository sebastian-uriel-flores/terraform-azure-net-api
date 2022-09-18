using Microsoft.EntityFrameworkCore;
using TerraAzSQLAPI.Models;

namespace TerraAzSQLAPI;

public class TareasContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    
    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Categorías
        var categoriasInit = new List<Categoria>
        {
            new Categoria 
            { 
                CategoriaID = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"),
                Nombre = "Actividades Pendientes",
                Peso = 20
            },
            new Categoria 
            { 
                CategoriaID = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"),
                Nombre = "Actividades Personales",
                Peso = 50
            }
        };
        modelBuilder.Entity<Categoria>(categoria => 
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaID);
            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(p => p.Descripcion).IsRequired(false);
            categoria.Property(p => p.Peso);

            categoria.HasData(categoriasInit);
        });

        // Tareas
        var tareasInit = new List<Tarea>
        {
            new Tarea
            {
                TareaID = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfb"),
                CategoriaId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"),
                PrioridadTarea = Prioridad.Media,
                Titulo = "Pago de servicios públicos",
                FechaCreacion = DateTime.Now
            },
            new Tarea
            {
                TareaID = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfc"),
                CategoriaId = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"),
                PrioridadTarea = Prioridad.Alta,
                Titulo = "Ir al dentista",
                FechaCreacion = DateTime.Now
            }
        };

        modelBuilder.Entity<Tarea>(tarea => 
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaID);
            tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
            tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
            tarea.Property(p => p.Descripcion);
            tarea.Property(p => p.PrioridadTarea);
            tarea.Property(p => p.FechaCreacion);
            tarea.Ignore(p => p.Resumen);

            tarea.HasData(tareasInit);
        });
    }
}