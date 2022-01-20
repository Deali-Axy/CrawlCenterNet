using CrawlCenter.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrawlCenter.Web.Extensions;

public static class ConfigureAppSettings {
    public static void AddSettings(this IServiceCollection services, IConfiguration configuration) {
        // 安全配置
        services.Configure<SecuritySettings>(configuration.GetSection(nameof(SecuritySettings)));
        // MongoDB配置
        services.Configure<MongodbSettings>(configuration.GetSection(nameof(MongodbSettings)));
    }
}