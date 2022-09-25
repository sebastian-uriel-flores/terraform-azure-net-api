using Microsoft.EntityFrameworkCore;
using DemoAPIAzure.Models;

namespace DemoAPIAzure;

public class ToDoContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<ToDo> ToDos { get; set; }

    
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var guid1 = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfb");
        var guid2 = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfc");

        // Categorías
        var categoriesInit = new List<Category>
        {
            new Category 
            { 
                CategoryId = guid1,
                Name = "Things to buy",
                Weight = 20
            },
            new Category 
            { 
                CategoryId = guid2,
                Name = "Platzi courses to do",
                Weight = 50
            }
        };
        modelBuilder.Entity<Category>(category => 
        {
            category.ToTable("Category");
            category.HasKey(p => p.CategoryId);
            category.Property(p => p.Name).IsRequired().HasMaxLength(150);
            category.Property(p => p.Description).IsRequired(false);
            category.Property(p => p.Weight);

            category.HasData(categoriesInit);
        });

        // Tareas
        var toDosInit = new List<ToDo>
        {
            new ToDo
            {
                ToDoId = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfa"),
                CategoryId = guid1,
                Priority = ToDoPriority.Middle,
                Title = "Milk",
                CreationDate = DateTime.Now
            },
            new ToDo
            {
                ToDoId = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfb"),
                CategoryId = guid1,
                Priority = ToDoPriority.High,
                Title = "Dog food",
                CreationDate = DateTime.Now
            },
            new ToDo
            {
                ToDoId = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfc"),
                CategoryId = guid2,
                Priority = ToDoPriority.High,
                Title = "Kubernetes",
                CreationDate = DateTime.Now
            },
            new ToDo
            {
                ToDoId = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfd"),
                CategoryId = guid2,
                Priority = ToDoPriority.High,
                Title = "New Relic",
                CreationDate = DateTime.Now
            },
            new ToDo
            {
                ToDoId = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfe"),
                CategoryId = guid2,
                Priority = ToDoPriority.High,
                Title = "Azure Databases",
                CreationDate = DateTime.Now
            }
        };

        modelBuilder.Entity<ToDo>(toDo => 
        {
            toDo.ToTable("ToDo");
            toDo.HasKey(p => p.ToDoId);
            toDo.HasOne(p => p.Category).WithMany(p => p.ToDos).HasForeignKey(p => p.CategoryId);
            toDo.Property(p => p.Title).IsRequired().HasMaxLength(200);
            toDo.Property(p => p.Description);
            toDo.Property(p => p.Priority);
            toDo.Property(p => p.CreationDate);            

            toDo.HasData(toDosInit);
        });
    }
}