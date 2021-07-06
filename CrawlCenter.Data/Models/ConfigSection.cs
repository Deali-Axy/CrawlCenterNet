using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrawlCenter.Data.Models {
    public class ConfigSection {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public IDictionary<string, ConfigKey> KeyValues { get; set; } = new Dictionary<string, ConfigKey>();
    }
}