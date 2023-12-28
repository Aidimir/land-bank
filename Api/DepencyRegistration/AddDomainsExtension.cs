using Logic.Features;
using Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Api.MIddlewares;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Dal;
using Dal.Models;
using Microsoft.AspNetCore.Identity;

namespace Api.DepencyRegistration
{
    public static class AddDomainServices
    {
        public static void AddLogicServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ILandService, LandService>();
            services.AddTransient<GlobalExceptionHandlerMiddleware>();
            services.TryAddScoped<HttpClient>();
            services.TryAddScoped<WebClient>();
        }
    }
}