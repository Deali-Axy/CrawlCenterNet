using CrawlCenter.Data.Models;
using CrawlCenter.Shared.Models;

namespace CrawlCenter.Data.Repositories.Impl;

public class ProjectConfigRepo : BaseConfigRepo {
    public ProjectConfigRepo(MongodbSettings settings) : base(settings) {
        Collection = Database.GetCollection<ConfigSection>(settings.Collections.ProjectConfig);
    }
}