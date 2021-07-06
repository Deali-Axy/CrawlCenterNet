// using CrawlCenter.Web.Data;
// using CrawlCenter.Web.Data.Repositories;
// using CrawlCenter.Web.Data.Repositories.Impl;
// using CrawlCenter.Web.Models;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace CrawlCenter.Web.Extensions {
//     public static class ConfigureEfCore {
//         public static void AddEfCore(this IServiceCollection services, IConfiguration configuration) {
//             // 添加数据库连接池
//             services.AddDbContextPool<AppDbContext>(options =>
//                 options.UseSqlite(configuration.GetConnectionString("SQLite")));
//             // 添加仓储模式配置
//             services.AddRepositories();
//         }
//     }
// }