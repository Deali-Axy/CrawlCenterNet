using CrawlCenter.Data.Models;
using CrawlCenter.Shared.Models;
using Microsoft.Extensions.Options;

namespace CrawlCenter.Data.Repositories.Impl;

public class CommonConfigRepo : BaseConfigRepo {
    public CommonConfigRepo(IOptions<MongodbSettings> options) : base(options) {
        Collection = Database.GetCollection<ConfigSection>(MongodbSettings.Collections.CommonConfig);
    }
}