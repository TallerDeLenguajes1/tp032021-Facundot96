using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Repositories;
using tp03.Models.Repositories.RepositorieSQLite;

namespace tp03
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddAutoMapper(typeof(CourierSystem.MappingProfile));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAuthorization();

            RepositoryDeliveryM RepoDeliveryM =
                new RepositoryDeliveryM(
                    Configuration.GetConnectionString("Default"),
                    NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            RepositoryCourier RepoCourier =
                new RepositoryCourier(
                    Configuration.GetConnectionString("Default"),
                    NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            RepositoryOrder RepoOrder =
                new RepositoryOrder(
                    Configuration.GetConnectionString("Default"),
                    NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            RepositoryUser RepoUser =
                new RepositoryUser(
                    Configuration.GetConnectionString("Default"),
                    NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            RepositoryClient RepoClient =
                new RepositoryClient(
                    Configuration.GetConnectionString("Default"),
                    NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            DataContext data = new DataContext(RepoDeliveryM, RepoOrder, RepoUser, RepoClient,RepoCourier);
            services.AddSingleton(data);

            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddMvc();
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
            app.UseSession();
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
