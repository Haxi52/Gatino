using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gatino.Web
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddSingleton<TopicRouteTransformer>();
            services.AddHttpContextAccessor();

            services.AddDbContextPool<Data.IDbContext, Data.DbContext>(options =>
            {
                var dbHost = config["db_host"] ?? "localhost";
                var dbName = config["db_name"] ?? "wiki";
                var dbUsername = config["db_username"] ?? "guest";
                var dbPassword = config["db_password"] ?? "guest";
                var dbPort = config["db_port"] ?? "1433";

                options.UseSqlServer($"Server={dbHost};Database={dbName};User Id={dbUsername};Password={dbPassword};Persist Security Info=False;MultipleActiveResultSets=true");
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDynamicControllerRoute<TopicRouteTransformer>("{*path}");
            });
        }
    }
}
