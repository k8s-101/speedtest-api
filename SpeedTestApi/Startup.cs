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
            services.AddDbContext<SpeedTestDbContext>(options => options.UseInMemoryDatabase("speedtest-api-database"));
            services.AddTransient<ISpeedTestDbService, SpeedTestDbService>();

            services.AddCors(
                options => options.AddPolicy(
                    AllowAllOrigins,
                    builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()));
            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(_apiInfo.Version, _apiInfo);
                })
                .AddMvc();
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

            application
                .UseCors(AllowAllOrigins)
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", _apiInfo.Title);
                })
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
