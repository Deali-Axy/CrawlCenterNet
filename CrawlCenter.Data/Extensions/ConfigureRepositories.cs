using CrawlCenter.Data.Repositories;
using CrawlCenter.Data.Repositories.Impl;
using CrawlCenter.Data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CrawlCenter.Data.Extensions {
    public static class ConfigureRepositories {
        public static void AddRepositories(this IServiceCollection services) {
            services.AddScoped<IRepository<ConfigSection, string>, ConfigRepo>();
            services.AddScoped<IAppRepository<CrawlTask>, CrawlTaskRepo>();
            services.AddScoped<IAppRepository<Project>, ProjectRepo>();
            services.AddScoped<IAppRepository<ProjectTag>, SqlRepository<ProjectTag>>();
            services.AddScoped<IAppRepository<RecurringTask>, RecurringTaskRepo>();
            services.AddScoped<IAppRepository<User>, SqlRepository<User>>();
        }
    }
}