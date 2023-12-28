using Dal;
using Dal.Models;
using Dal.Repositories;
using Dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.DepencyRegistration
{
    public static class AddRepositoriesExtension
    {
        public static void AddRepositories(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<UserContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();

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

