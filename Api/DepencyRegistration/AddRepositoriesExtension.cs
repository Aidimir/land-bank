using Dal.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.DepencyRegistration
{
    public static class AddRepositoriesExtension
    {
        public static void AddRepositories(this IServiceCollection services, string? connectionString)
        {
            //services.AddDbContext<IMainDatabase, MainDatabase>(options =>
            //{
            //    options.UseNpgsql(connectionString);
            //});

            //using (var provider = services.BuildServiceProvider())
            //{
            //    var service = provider.GetRequiredService<MainDatabase>();
            //    if (service.Database.GetPendingMigrations().Any())
            //        service.Database.Migrate();
            //}
        }
    }
}

