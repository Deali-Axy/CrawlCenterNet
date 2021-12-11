using CrawlCenter.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CrawlCenter.Data.Extensions {
    public static class ConfigureMongoDB {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<MongoDBSettings>(configuration.GetSection(nameof(MongoDBSettings)));
            services.AddSingleton<MongoDBSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
        }
    }
}