using HealthService.API.Data;
using HealthService.API.Infrastructure.Repositories;
using HealthService.API.Models.Analysis;
using HealthService.API.Models.Users;
using HealthService.API.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

using Watchman.API.Common.Attributes;
using Watchman.API.Common.Extensions;
using Watchman.API.Common.Services.JWT;
using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Services;

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

            string healthConnection = Configuration.GetConnectionString("HealthDbConnection");
            services.AddDbContext<HealthDbContext>(options => options.UseSqlServer(healthConnection));


            services.AddScoped<ValidationModelStateActionFilterAttribute>();

            services.AddTransient<IAnalysisResult, AnalysisResult>();
            services.AddTransient<IAnalysisStrategy, MinMaxValueAnalyseStrategy>();
            services.AddTransient<IHealthAnalyzer, HealthAnalyzer>();
            services.AddTransient<IControlRequestRepository, ControlRequestRepository>();
            services.AddTransient<IWatchmanRepository<WatchmanProfileHealth, Guid>, WatchmanRepository>();
            services.AddTransient<IPatientRepository<PatientProfile, Guid>, PatientRepository>();
            services.AddTransient<IWatchmanPatientUnitOfWork, WatchmanPatientUnitOfWork>();
            services.AddTransient<IControlRequestService, ControlRequestService>();
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
