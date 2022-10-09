using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoAPIAzure.Entities.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            var guid1 = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfb");
            var guid2 = Guid.Parse("7c2196e4-9d06-4574-a212-d4bdef0a4bfc");

            var categoriesInit = new List<Category>
            {
                new Category 
                { 
                    CategoryId = guid1,
                    Name = "Things to buy",
                    Description = "All the things I want to buy",
                    Weight = 20
                },
                new Category 
                { 
                    CategoryId = guid2,
                    Name = "Platzi courses to do",
                    Description = "All the Platzi courses that I consider fun or that will be useful for muy proffesion",                    
                    Weight = 50
                }
            };

            builder.HasKey(p => p.CategoryId);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(p => p.Description);
            builder.Property(p => p.Weight);

            builder.HasData(categoriesInit);
        }
    }
}
