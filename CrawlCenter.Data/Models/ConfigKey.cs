using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CrawlCenter.Data.Models; 

public class ConfigKey {
    [BsonId]
    public string Id => Name;
    public string Name { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }
}