using HealthService.API.Models;
using HealthService.API.Models.Analysis;
using HealthService.API.Models.Data;
using HealthService.API.Models.Extensions;
using HealthService.API.Models.Repositories;
using HealthService.API.Models.Users;
using HealthService.API.Services;

using HealthServices.API.Models.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Data;

namespace HealthService.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new JwtValidator(Configuration).GetValidationParameters();
               });

            services.AddMvc();
            services.ConfigureCors();

            string healthConnection = Configuration.GetConnectionString("UserDbConnection");
            services.AddDbContext<HealthDbContext>(options => options.UseSqlServer(healthConnection));

            string usersConnection = Configuration.GetConnectionString("UserDbConnection");
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(usersConnection));

            services.AddScoped<ValidationModelStateActionFilterAttribute>();            

            services.AddTransient<IAnalysisResult, AnalysisResult>();
            services.AddTransient<IAnalysisStrategy, MinMaxValueAnalyseStrategy>();
            services.AddTransient<IHealthAnalyzer, HealthAnalyzer>();
            services.AddTransient<IWatchmanRepository<WatchmanProfileHealth, Guid>, WatchmanRepository>();
            services.AddTransient<IPatientRepository<PatientProfile, Guid>, PatientRepository>();
            services.AddTransient<IWatchmanPatientUnitOfWork, WatchmanPatientUnitOfWork>();
            services.AddTransient<IWatchmanPatientService<Guid>, WatchmanPatientService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action}");
            });
        }
    }
}
