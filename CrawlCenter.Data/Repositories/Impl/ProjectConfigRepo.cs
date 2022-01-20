using CrawlCenter.Data.Models;
using CrawlCenter.Shared.Models;
using Microsoft.Extensions.Options;

namespace CrawlCenter.Data.Repositories.Impl;

public class ProjectConfigRepo : BaseConfigRepo {
    public ProjectConfigRepo(IOptions<MongodbSettings> options) : base(options) {
        Collection = Database.GetCollection<ConfigSection>(MongodbSettings.Collections.ProjectConfig);
    }
}