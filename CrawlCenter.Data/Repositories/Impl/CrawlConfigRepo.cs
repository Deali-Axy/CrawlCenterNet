using CrawlCenter.Data.Models;
using CrawlCenter.Shared.Models;

namespace CrawlCenter.Data.Repositories.Impl;

public class CrawlConfigRepo : BaseConfigRepo {
    public CrawlConfigRepo(MongodbSettings settings) : base(settings) {
        Collection = Database.GetCollection<ConfigSection>(settings.Collections.CrawlTaskConfig);
    }
}