using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpeedTestApi.Database;
using SpeedTestApi.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SpeedTestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SpeedTestDbContext>(options => options.UseInMemoryDatabase("speedtest-api-database"));
            services.AddTransient<ISpeedTestDbService, SpeedTestDbService>();

            services
                .AddCors()
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new Info { Title = "SpeedTestApi", Version = "v1" });
                })
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder application, IHostingEnvironment environment)
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

            application.UseCors(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
                policy.AllowCredentials();
            });

            application
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "speedtest-api");
                })
                .UseSwagger()
                .UseMvc();
        }
    }
}
