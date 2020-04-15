using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Models;
using Watchman.Web.Services;

namespace Watchman.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                 options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
             });

            services.AddControllersWithViews();
            services.AddTransient<HttpClient>();
            services.AddTransient<PersonalInformation<Guid>, PersonalInfo>();
            services.AddTransient<IHttpClient, WatchmanHttpClient>();
            services.AddTransient<IUserWatchmanPatientService<Guid>, WatchmanPatientService>();//TODO: create other service
            services.AddTransient<IWatchmanPatientService<Guid>, WatchmanPatientService>();//
            services.AddTransient<IJwtValidator, JwtValidator>();
            services.AddTransient<IUserManager<WatchmanUser, Guid>, UserManager>();
            services.AddTransient<IPersonalInformationService<PersonalInfo, Guid>, PersonalInfoService>();
            services.AddTransient<ITokenService, TokenService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
