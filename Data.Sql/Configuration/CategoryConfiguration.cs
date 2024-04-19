using Data.Interface.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Sql.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(c => c.Subcategories)
                .WithOne(s => s.Category);

            builder
                .HasMany(c => c.Specifications)
                .WithOne(s => s.Category);
        }
    }
}
