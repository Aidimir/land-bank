using Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;

namespace Api.DepencyRegistration
{
    public static class AddDomainServices
    {
        public static void AddLogicServices(this IServiceCollection services)
        {
            services.TryAddScoped<HttpClient>();
            services.TryAddScoped<WebClient>();
        }
    }
}