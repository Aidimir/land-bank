using Logic.Features;
using Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;

namespace Api.DepencyRegistration
{
    public static class AddDomainServices
    {
        public static void AddLogicServices(this IServiceCollection services)
        {
            services.AddTransient<ILandService, LandService>();
            services.TryAddScoped<HttpClient>();
            services.TryAddScoped<WebClient>();
        }
    }
}