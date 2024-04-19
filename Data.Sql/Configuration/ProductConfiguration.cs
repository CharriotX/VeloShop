using Data.Interface.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

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
                .HasMany(p => p.Specifications)
                .WithMany(s => s.Products)
                .UsingEntity<ProductSpecification>(
                    r => r.HasOne<Specification>().WithMany().HasForeignKey(s => s.SpecificationId),
                    l => l.HasOne<Product>().WithMany().HasForeignKey(p => p.ProductId)
                );
        }
    }
}
