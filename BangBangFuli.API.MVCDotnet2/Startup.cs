using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Extensions;
using BangBangFuli.H5.API.Core;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.EntityFrameworkCore;
using BangBangFuli.Utils.ORM.Imp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using UEditorNetCore;

namespace BangBangFuli.API.MVCDotnet2
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

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "bangbangfuli",
                    Version = "v1"
                }
                );
            });

            services.AddDbContext<CouponSystemDBContext>(d => d.UseSqlServer(Configuration.GetConnectionString("H5BasicData")));
            services.AddScoped<IUnitOfWork, H5.API.EntityFrameworkCore.UnitOfWork<CouponSystemDBContext>>();//注入UOW依赖，确保每次请求都是同一个对象
            services.AddByAssembly("BangBangFuli.H5.API.EntityFrameworkCore", "IBaseRepository");
            services.AddByAssembly("BangBangFuli.H5.API.Application", "IAppService");
            services.AddUEditorService();
            services.AddSession();

            // bug fix 解决dotnetcore中， webapi返回的json,自动把首字母的大写给转成小写了
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());


            //跨域
            services.AddCors(o => o.AddPolicy("any", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();
            //启用认证
            app.UseAuthentication();
            //用跨域
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI(m=> {
                m.SwaggerEndpoint("/swagger/v1/swagger.json", "swaggerTest");
            });
          
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=EnterCustom}/{action=ConsoleIndex}/{id?}");
                    //template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
