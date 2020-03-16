using Identity.API.Data;
using Identity.API.Models;
using Identity.API.Models.Extensions;
using Identity.API.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

using Watchman.BusinessLogic.Models.Users;

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
            services.AddMvc();
            services.ConfigureCors();


            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<WatchmanDbContext>(options => options.UseSqlServer(connection));

            services.AddTransient<IPersonalInformation<Guid>, PersonalInformation>();
            services.AddTransient<IUser<PersonalInformation>, WatchmanUser>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICustomPasswordHasher, PasswordHasher>();
            services.AddTransient<ILoginService<WatchmanUser, Guid>, LoginService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}");
            });
        }
    }
}
