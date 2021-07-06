using System;
using System.Collections.Generic;
using CrawlCenter.Data.Models;
using MongoDB.Driver;

namespace CrawlCenter.Data.Repositories.Impl {
    public class ConfigRepo : IRepository<ConfigSection, string> {
        private readonly IMongoCollection<ConfigSection> _configSections;

        public ConfigRepo(MongoDBSettings settings) {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _configSections = database.GetCollection<ConfigSection>(settings.ConfigCollectionName);
        }

        public ConfigSection GetById(string id) {
            return _configSections.Find(a => a.Id == id).FirstOrDefault();
        }
        
        public IEnumerable<ConfigSection> GetAll() {
            return _configSections.Find(a => true).ToList();
        }

        public int Insert(ConfigSection obj) {
            _configSections.InsertOne(obj);
            return 1;
        }

        public int Update(ConfigSection obj) {
            var result= _configSections.ReplaceOne(item => item.Id == obj.Id, obj);
            return (int)result.ModifiedCount;
        }

        public int Delete(string id) {
            var result= _configSections.DeleteOne(a => a.Id == id);
            return (int) result.DeletedCount;
        }
    }
}