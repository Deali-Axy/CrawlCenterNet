using CrawlCenter.Data.Models;
using CrawlCenter.Shared.Models;
using Microsoft.Extensions.Options;

namespace CrawlCenter.Data.Repositories.Impl;

/// <summary>
/// 爬虫环境变量
/// </summary>
public class CrawlEnvRepo : BaseConfigRepo {
    public CrawlEnvRepo(IOptions<MongodbSettings> options) : base(options) {
        Collection = Database.GetCollection<ConfigSection>(MongodbSettings.Collections.CrawlEnvVar);
    }
}