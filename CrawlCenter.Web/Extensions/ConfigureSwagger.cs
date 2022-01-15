using System.IO;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CrawlCenter.Web.Extensions {
    public static class ConfigureSwagger {
        public static void AddSwagger(this IServiceCollection services) {
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CrawlCenter.Web API V1", Version = "v1" });
                options.AddServer(new OpenApiServer {
                    Url = "",
                    Description = "vvv"
                });
                options.CustomOperationIds(apiDesc => {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return $"{controllerAction?.ControllerName}-{controllerAction?.ActionName}";
                });

                // 为 Swagger JSON and UI设置xml文档注释路径
                var xmlPath = Path.Combine(System.AppContext.BaseDirectory, "CrawlCenter.Web.xml");
                options.IncludeXmlComments(xmlPath, true);
            });
        }
    }
}