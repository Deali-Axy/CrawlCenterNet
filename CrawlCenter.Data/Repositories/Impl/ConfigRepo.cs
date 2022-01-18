using System;
using System.Collections.Generic;
using CrawlCenter.Data.Models;
using MongoDB.Driver;

namespace CrawlCenter.Data.Repositories.Impl {
    public class ConfigRepo : IRepository<ConfigSection, string> {
        private readonly IMongoCollection<ConfigSection> _collection;

        public ConfigSection this[string name] {
            get => GetByName(name);
            set {
                if (GetByName(name) != null)
                    Update(value);
                else
                    Insert(value);
            }
        }

        public ConfigKey this[string sectionName, string keyName] {
            get {
                var section = GetByName(sectionName);
                return section?[keyName];
            }
            set {
                var section = GetByName(sectionName);
                if (section == null) {
                    section = new ConfigSection {
                        Name = sectionName,
                        [keyName] = value
                    };
                    Insert(section);
                }
                else {
                    var updateFlag = !section.KeyValues.ContainsKey(keyName);
                    section[keyName] = value;
                    if (updateFlag) Update(section);
                }
            }
        }

        public ConfigRepo(MongoDBSettings settings) {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<ConfigSection>(settings.ConfigCollectionName);
        }

        public bool HasSection(string name) {
            return GetByName(name) != null;
        }

        public ConfigSection GetByName(string name) {
            return _collection.Find(a => a.Name == name).FirstOrDefault();
        }

        public ConfigSection GetById(string id) {
            return _collection.Find(a => a.Id == id).FirstOrDefault();
        }

        public IEnumerable<ConfigSection> GetAll() {
            return _collection.Find(a => true).ToList();
        }

        public int Insert(ConfigSection obj) {
            _collection.InsertOne(obj);
            return 1;
        }

        public int Update(ConfigSection obj) {
            var result = _collection.ReplaceOne(item => item.Id == obj.Id, obj);
            return (int)result.ModifiedCount;
        }

        public int Delete(string id) {
            var result = _collection.DeleteOne(a => a.Id == id);
            return (int)result.DeletedCount;
        }
    }
}