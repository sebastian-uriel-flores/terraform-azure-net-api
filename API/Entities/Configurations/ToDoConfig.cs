using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoAPIAzure.Entities.Configurations
{
    public class ToDoConfig : IEntityTypeConfiguration<ToDo>
    {
        public void Configure(EntityTypeBuilder<ToDo> builder)
        {
            var guid1 = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfb");
            var guid2 = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfc");

            var toDosInit = new List<ToDo>
            {
                new ToDo
                {
                    ToDoId = Guid.Parse("8c2196e4-9d06-4574-a212-d4bdef0a4bfa"),
                    CategoryId = guid1,
                    Priority = ToDoPriority.Middle,
                    Title = "Milk",
                    Description = "Nestle",
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
                    Description = "k8s",
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

            builder.HasKey(p => p.ToDoId);            
            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(p => p.Description);
            builder.Property(p => p.Priority);
            builder.Property(p => p.CreationDate);            

            builder.HasData(toDosInit);          
        }
    }
}
