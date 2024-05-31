using Data.Interface.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Sql.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");

            builder
               .HasOne(p => p.Category)
               .WithMany(c => c.Products);

            builder
               .HasOne(p => p.Subcategory)
               .WithMany(c => c.Products);

            builder
                .HasOne(p => p.Brand)
                .WithMany(c => c.Products);

            builder
                .HasMany(p => p.Specifications)
                .WithMany(s => s.Products)
                .UsingEntity<ProductSpecification>(
                    r => r.HasOne<Specification>().WithMany(e => e.ProductSpecifications),
                    l => l.HasOne<Product>().WithMany(e => e.ProductSpecifications)
                );
        }
    }
}
