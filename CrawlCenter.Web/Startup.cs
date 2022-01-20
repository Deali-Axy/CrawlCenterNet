using System;
using CrawlCenter.Contrib.WebMessages;
using CrawlCenter.Data.Extensions;
using CrawlCenter.Data.Models;
using CrawlCenter.Web.Extensions;
using CrawlCenter.Web.Middlewares;
using Exceptionless;
using Hangfire;
using IGeekFan.AspNetCore.Knife4jUI;
using IGeekFan.AspNetCore.RapiDoc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CrawlCenter.Web {
    public class Startup {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env) {
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

            services.AddSettings(_configuration);

            services.AddHangfire(_configuration);
            services.AddSwagger();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuth(_configuration);

            // 添加消息框架
            services.AddSingleton<Messages>();
            // 添加FreeSQL
            services.AddFreeSql(_configuration);
            // 添加仓储
            services.AddRepositories();

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
            app.UseSwaggerUI(c => {
                c.RoutePrefix = "docs/swagger";
                c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
            });
            app.UseKnife4UI(c => {
                c.RoutePrefix = "docs/knife4j";
                c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
            });
            app.UseRapiDocUI(c => {
                c.RoutePrefix = "docs/rapi";
                c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
                c.GenericRapiConfig = new GenericRapiConfig {
                    Theme = "light"
                };
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