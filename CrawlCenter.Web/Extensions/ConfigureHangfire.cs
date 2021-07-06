using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrawlCenter.Web.Extensions {
    public static class ConfigureHangfire {
        public static void AddHangfire(this IServiceCollection services,IConfiguration configuration) {
            // Add Hangfire services.
            services.AddHangfire(conf => conf
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(new SQLiteStorage(configuration.GetConnectionString("HangfireSqlite")))
            );
            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }
    }
}