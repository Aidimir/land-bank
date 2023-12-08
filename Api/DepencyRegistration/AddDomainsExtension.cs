using Logic.Features;
using Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Api.MIddlewares;
using System.Net;

namespace Api.DepencyRegistration
{
    public static class AddDomainServices
    {
        public static void AddLogicServices(this IServiceCollection services)
        {
            services.AddTransient<ILandService, LandService>();
            services.AddTransient<GlobalExceptionHandlerMiddleware>();
            services.TryAddScoped<HttpClient>();
            services.TryAddScoped<WebClient>();
        }
    }
}