using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Extensions;
using BangBangFuli.Common;
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
                    Title = @"ClassType:商品大类       
                        yuexiangmeiwei=0, 悦享美味
                        jujiahaowu=1,  居家好物
                        pingzhishenghuo=2, 品质生活
                        chufangzhengxuan=3,  厨房甑选
                        unknown=4 ,未知类别
                        /// -----------------------
                         StockStatus:库存状态
                         No=0,  没有货
                         Yes=1,  有货
                         Unknown=2，未知状态
                        /// -----------------------
                         ProductStatus:商品状态
                         Down=0, 未上架
                         On=1, 上架
                         Unknown=2，未知状态 ",
                    Version = "v1"
                }
                );
            });

            /*
             * bug fix SqlException: 'OFFSET' 附近有语法错误。 在 FETCH 语句中选项 NEXT 的用法无效。
            这个主要是在sql server 2008中，不支持FETCH和NEXT语句（sql server 2012才支持）。
             */
            services.AddDbContext<CouponSystemDBContext>(d => d.UseSqlServer(Configuration.GetConnectionString("H5BasicData"),b=> b.UseRowNumberForPaging()));
            services.AddScoped<IUnitOfWork, H5.API.EntityFrameworkCore.UnitOfWork<CouponSystemDBContext>>();//注入UOW依赖，确保每次请求都是同一个对象
            services.AddByAssembly("BangBangFuli.H5.API.EntityFrameworkCore", "IBaseRepository");
            services.AddByAssembly("BangBangFuli.H5.API.Application", "IAppService");
            services.AddSingleton<IRabbitMqProducer, RabbitMqProducer>();
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
