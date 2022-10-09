using Microsoft.EntityFrameworkCore;
using DemoAPIAzure.Entities;
using System.Reflection;

namespace DemoAPIAzure;

public class ToDoContext : DbContext
{
    
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<ToDo> ToDos { get; set; }
}