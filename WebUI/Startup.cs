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
using DL;
using BL;
using Microsoft.EntityFrameworkCore;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // transient, scoped, singleton
        // Transient: a new object is created everytime you call
        // Scoped: new object per request
        // Singleton: share an instance across request -> could lead to other requests waiting
        // Containter that holds the resources that the app needs
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<HFMPBDBContext>(options => options.UseNpgsql(Configuration.GetConnectionString("HatsDB")));
            // secondly, we need to configure our db here and add the db context as a dependency
            // finally, we add all the other dependencies, such as BL, repos.
            // This uses inversion of control, which means that we specify what kind of 
            // concrete classes implement interfaces.
            services.AddScoped<IRepo, DBRepo>();
            services.AddScoped<IBL, BBBBL>();
            //^ that is our dependency injection. We are "registering" the dependencies that our controllers need
        }

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Customer}/{action=SignIn}/{id?}");
            });
        }
    }
}
