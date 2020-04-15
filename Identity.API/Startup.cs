using Identity.API.Data;
using Identity.API.Infrastructure.Repositories;
using Identity.API.Models;
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
using Watchman.API.Common.Attributes;
using Watchman.API.Common.Extensions;
using Watchman.API.Common.Services.JWT;
using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Services;

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


            string connection = Configuration.GetConnectionString("UserDb");
            services.AddDbContext<WatchmanDbContext>(options => options.UseSqlServer(connection));

            services.AddScoped<ValidationModelStateActionFilterAttribute>();

            services.AddTransient<IPersonalInformationRepository<PersonalInfo, Guid>, PersonalInfoRepository>();
            services.AddTransient<IUserRepository<IdentityUser>, UserRepository>();
            services.AddTransient<ICustomPasswordHasher, PasswordHasher>();
            services.AddTransient<IUserManager<IdentityUser, Guid>, UserManager>();
            services.AddTransient<ILoginService<IdentityUser, Guid>, LoginService>();
            services.AddTransient<IUserWatchmanPatientService<Guid>, UserWatchmanPatientService>();
            services.AddTransient<IRoleService<Guid>, RoleService>();
            services.AddTransient<IJwtValidator, JwtValidator>();
            services.AddTransient<IJwtGenerator, JwtGenerator>();
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
