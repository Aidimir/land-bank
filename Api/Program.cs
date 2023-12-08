using Api.DepencyRegistration;
using Api.MIddlewares;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Land-bank API",
    });
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var fullpath = $"{AppDomain.CurrentDomain.BaseDirectory + xmlFilename}";
    //options.IncludeXmlComments(fullpath);
});

builder.Services.AddLogicServices();
builder.Services.AddRepositories(builder.Configuration.GetConnectionString("WebApiDatabase"));

var app = builder.Build();

app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );

app.UseSwagger(option => option.SerializeAsV2 = true);
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
