using Data.Interface.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Sql.Configuration
{
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(t => t.User)
                .WithOne(u => u.Token)
                .HasForeignKey<Token>(x => x.UserId);
        }
    }
}
