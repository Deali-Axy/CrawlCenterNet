using CrawlCenter.Data.Models;
using CrawlCenter.Shared.Models;

namespace CrawlCenter.Data.Repositories.Impl; 

public class CommonConfigRepo : BaseConfigRepo {
    public CommonConfigRepo(MongodbSettings settings) : base(settings) {
        Collection = Database.GetCollection<ConfigSection>(settings.Collections.CommonConfig);
    }
}