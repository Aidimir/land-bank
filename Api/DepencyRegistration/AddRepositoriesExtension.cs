using Dal.Repositories;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.DepencyRegistration
{
    public static class AddRepositoriesExtension
    {
        public static void AddRepositories(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<ILandRepository, LandRepository>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            using (var provider = services.BuildServiceProvider())
            {
                var service = provider.GetRequiredService<LandRepository>();
                if (service.Database.GetPendingMigrations().Any())
                    service.Database.Migrate();
            }
        }
    }
}

