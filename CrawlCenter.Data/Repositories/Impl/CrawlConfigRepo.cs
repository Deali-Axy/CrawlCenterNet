using CrawlCenter.Data.Models;
using CrawlCenter.Shared.Models;
using Microsoft.Extensions.Options;

namespace CrawlCenter.Data.Repositories.Impl;

public class CrawlConfigRepo : BaseConfigRepo {
    public CrawlConfigRepo(IOptions<MongodbSettings> options) : base(options) {
        Collection = Database.GetCollection<ConfigSection>(MongodbSettings.Collections.CrawlTaskConfig);
    }
}