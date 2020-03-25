using Identity.API.Data;
using Identity.API.Models;
using Identity.API.Models.Extensions;
using Identity.API.Repositories;
using Identity.API.Services;
using Identity.API.Services.JWT;
using Identity.API.Services.PasswordHashing;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

using Watchman.BusinessLogic.Models.Data;

namespace Identity.API
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


            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<WatchmanDbContext>(options => options.UseSqlServer(connection));

            services.AddTransient<IUserRepository<WatchmanUser>, UserRepository>();
            services.AddTransient<ICustomPasswordHasher, PasswordHasher>();
            services.AddTransient<ILoginService<WatchmanUser, Guid>, LoginService>();
            services.AddTransient<IJwtValidator, JwtValidator>();
            services.AddTransient<IJwtGenerator, JwtGenerator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
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
