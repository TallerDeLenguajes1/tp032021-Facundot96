using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TP3.Models.Repositories.RepositoriesSQLite;
using NLog.Web;
using TP3.Models.Repositories;

namespace TP3
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAuthorization();
            services.AddAutoMapper(typeof(TP3.MappingProfile));

            RepositorieDeliveryM RepoDeliverM =
                new RepositorieDeliveryM(
                    Configuration.GetConnectionString("Default"),
                    NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            RepositorieCourier RepoCourier =
                new RepositorieCourier(
                    Configuration.GetConnectionString("Default"),
                    NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            RepositorieOrder RepoOrder =
                new RepositorieOrder(
                    Configuration.GetConnectionString("Default"),
                    NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            RepositorieUser RepoUser =
                new RepositorieUser(
                    Configuration.GetConnectionString("Default"),
                    NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            RepositorieClient RepoClient =
                new RepositorieClient(
                    Configuration.GetConnectionString("Default"),
                    NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            DataContext data = new DataContext(RepoClient, RepoCourier, RepoDeliverM, RepoOrder, RepoUser);
            services.AddSingleton(data);

            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
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
                app.UseExceptionHandler("/Error");
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
