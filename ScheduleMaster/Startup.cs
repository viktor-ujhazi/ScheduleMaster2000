using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ScheduleMaster.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using Npgsql;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ScheduleMaster
{
    public class Startup
    {
        private readonly string connectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            connectionString = InitConnectionString();
        }

        public IConfiguration Configuration { get; }

        private string InitConnectionString()
        {
            string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "Host=localhost;Username=postgres;Password=admin;Database=ScheduleMaster";
            return connectionString;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<ICyberSecurityProvider, PasswordEncrypter>();
            services.AddScoped<IDbConnection>(_ =>
            {
                var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                return connection;
            });
            services.AddScoped<IUsersService, SQLUsersService>();
            services.AddScoped<ITasksService, SQLTasksService>();
            services.AddScoped<ISchedulesService, SQLSchedulesService>();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
            (CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/User/Login";
                options.LogoutPath = "/User/Logout";
            }
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
