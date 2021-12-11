using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrawlCenter.Data.Extensions {
    public static class ConfigureFreeSql {
        public static void AddFreeSql(this IServiceCollection services, IConfiguration configuration) {
            var freeSql = new FreeSqlBuilder()
                .UseConnectionString(DataType.Sqlite, configuration.GetConnectionString("SQLite"))
                .UseAutoSyncStructure(true)
                .Build();
            
            services.AddSingleton(freeSql);
            
            // 仓储模式支持
            services.AddFreeRepository();
            
            // DBContext模式
            services.AddFreeDbContext<AppDbContext>(options => options.UseFreeSql(freeSql));
        }
    }
}