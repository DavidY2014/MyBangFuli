using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BangBangFuli.API.MVC.Extensions;
using BangBangFuli.H5.API.EntityFrameworkCore;
using BangBangFuli.Utils.ORM.Imp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BangBangFuli.API.MVC
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
            //services.AddDbContext<CouponSystemDBContext>(d => d.UseSqlServer(Configuration.GetConnectionString("H5BasicData")));
            
            services.AddScoped<IDbContextManager<CouponSystemDBContext>>(s =>
            {
                return new DbContextManager<CouponSystemDBContext>(new ConnectionOption()
                {
                    Master = Configuration.GetConnectionString("H5BasicData"),
                    SqlProvider = SqlProvider.SqlServer
                });
            });

            //services.AddControllersWithViews();
            services.AddByAssembly("BangBangFuli.H5.API.EntityFrameworkCore", "IBaseRepository");
            services.AddByAssembly("BangBangFuli.H5.API.Application", "IAppService");
            services.AddMvc();

            #region ¼Óswagger


            #endregion

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
            }
            app.UseStaticFiles();

            app.UseRouting();

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
