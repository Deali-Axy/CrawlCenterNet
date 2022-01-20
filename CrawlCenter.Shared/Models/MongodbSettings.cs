namespace CrawlCenter.Shared.Models;

public class MongodbSettings {
    public string ConfigCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }

    public Collections Collections { get; set; }
}

public class Collections {
    public string CommonConfig { get; set; }
    public string ProjectConfig { get; set; }
    public string CrawlTaskConfig { get; set; }
    public string CrawlEnvVar { get; set; }
}