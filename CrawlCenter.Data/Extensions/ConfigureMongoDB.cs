using CrawlCenter.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CrawlCenter.Data.Extensions {
    public static class ConfigureMongoDB {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton<MongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
        }
    }
}