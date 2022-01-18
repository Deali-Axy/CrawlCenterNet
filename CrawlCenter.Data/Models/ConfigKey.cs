using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CrawlCenter.Data.Models {
    public class ConfigKey {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}