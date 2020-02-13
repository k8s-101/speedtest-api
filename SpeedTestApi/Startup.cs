using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SpeedTestApi.Database;
using SpeedTestApi.Services;

namespace SpeedTestApi
{
    public class Startup
    {
        private const string AllowAllOrigins = "AllowAllOrigins";

        private readonly OpenApiInfo _apiInfo = new OpenApiInfo
        {
            Version = "v1",
            Title = "SpeedTestApi",
            Description = "An API for storing SpeedTests.",
        };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<SpeedTestDbContext>(options => options.UseInMemoryDatabase("speedtest-api-database"));
            services.AddTransient<ISpeedTestDbService, SpeedTestDbService>();

            services.AddCors(
                options => options.AddPolicy(
                    AllowAllOrigins,
                    builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()));

            services.AddSwaggerGen(options =>
                options.SwaggerDoc(_apiInfo.Version, _apiInfo));
        }

        public void Configure(IApplicationBuilder application, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                application
                    .UseDeveloperExceptionPage()
                    .UseDummyDataSeed();
            }
            else
            {
                application
                    .UseHsts()
                    .UseHttpsRedirection();
            }

            var basePath = Configuration["basePath"];
            application.UseSwagger(c =>
                {
                    c.RouteTemplate = "swagger/{documentName}/swagger.json";
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        var httpsUrl = $"https://{httpReq.Host.Value}{basePath}";
                        var httpUrl = $"http://{httpReq.Host.Value}{basePath}";
                        swaggerDoc.Servers = new OpenApiServer[]
                        {
                            new OpenApiServer { Url = httpsUrl },
                            new OpenApiServer { Url = httpUrl },
                        };
                    });
                });

            var swaggerEndpointUrl = $"{basePath.TrimEnd('/')}/swagger/v1/swagger.json";
            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerEndpointUrl, _apiInfo.Title);
            });

            application
                .UseCors(AllowAllOrigins)
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
