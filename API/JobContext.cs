using Microsoft.EntityFrameworkCore;
using DemoAPIAzure.Models;

namespace DemoAPIAzure;

public class JobContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Job> Jobs { get; set; }

    
    public JobContext(DbContextOptions<JobContext> options) : base(options) { }

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
        var jobsInit = new List<Job>
        {
            new Job
            {
                JobId = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfa"),
                CategoryId = guid1,
                Priority = JobPriority.Middle,
                Title = "Milk",
                CreationDate = DateTime.Now
            },
            new Job
            {
                JobId = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfb"),
                CategoryId = guid1,
                Priority = JobPriority.High,
                Title = "Dog food",
                CreationDate = DateTime.Now
            },
            new Job
            {
                JobId = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfc"),
                CategoryId = guid2,
                Priority = JobPriority.High,
                Title = "Kubernetes",
                CreationDate = DateTime.Now
            },
            new Job
            {
                JobId = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfd"),
                CategoryId = guid2,
                Priority = JobPriority.High,
                Title = "New Relic",
                CreationDate = DateTime.Now
            },
            new Job
            {
                JobId = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfe"),
                CategoryId = guid2,
                Priority = JobPriority.High,
                Title = "Azure Databases",
                CreationDate = DateTime.Now
            }
        };

        modelBuilder.Entity<Job>(job => 
        {
            job.ToTable("Job");
            job.HasKey(p => p.JobId);
            job.HasOne(p => p.Category).WithMany(p => p.Jobs).HasForeignKey(p => p.CategoryId);
            job.Property(p => p.Title).IsRequired().HasMaxLength(200);
            job.Property(p => p.Description);
            job.Property(p => p.Priority);
            job.Property(p => p.CreationDate);            

            job.HasData(jobsInit);
        });
    }
}