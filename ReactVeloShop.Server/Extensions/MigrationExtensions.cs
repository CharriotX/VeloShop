using Data.Sql;
using Microsoft.EntityFrameworkCore;

namespace ReactVeloShop.Server.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using WebContext dbContext = scope.ServiceProvider.GetRequiredService<WebContext>();

            dbContext.Database.Migrate();
        }
    }
}
