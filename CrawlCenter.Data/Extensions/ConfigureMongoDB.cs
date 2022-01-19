using CrawlCenter.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CrawlCenter.Data.Extensions {
    public static class ConfigureMongoDB {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<MongodbSettings>(configuration.GetSection(nameof(MongodbSettings)));
            services.AddSingleton<MongodbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongodbSettings>>().Value);
        }
    }
}