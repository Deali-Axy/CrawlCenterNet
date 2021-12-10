using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CrawlCenter.Contrib.WebMessages;
using CrawlCenter.Data;
using CrawlCenter.Data.Extensions;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Web.Extensions;
using CrawlCenter.Web.Middlewares;
using Exceptionless;
using FreeSql;
using Hangfire;
using Hangfire.MySql;
using Hangfire.Storage.SQLite;
using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace CrawlCenter.Web {
    public class Startup {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration,IWebHostEnvironment env) {
            _configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var builder = services.AddControllersWithViews();
            if (_env.IsDevelopment()) {
                // 添加Razor页面运行时动态编译支持
                builder.AddRazorRuntimeCompilation();
            }
            services.AddHangfire(_configuration);
            services.AddExceptionless();
            services.AddSwagger();

            // 添加消息框架
            services.AddSingleton<Messages>();

            // 添加FreeSQL
            services.AddFreeSql(_configuration);
            
            // 添加仓储
            services.AddRepositories();

            services.Configure<MongoDBSettings>(_configuration.GetSection(nameof(MongoDBSettings)));
            services.AddSingleton<MongoDBSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            services.AddExceptionless(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseExceptionless();
            app.UseMiddleware<PrintRequestMiddleware>();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Error/StatusCode/{0}");

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireDashboard();

            app.UseSwagger();

            app.UseKnife4UI(c => {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
            });

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapSwagger("{documentName}/api-docs");

                // endpoints.MapHangfireDashboard();
            });
        }
    }
}