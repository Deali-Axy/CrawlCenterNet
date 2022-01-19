using System;
using System.Collections.Generic;
using CrawlCenter.Data.Models;
using CrawlCenter.Shared.Models;
using MongoDB.Driver;

namespace CrawlCenter.Data.Repositories.Impl;

/// <summary>
/// 配置中心仓库 实现基类
/// </summary>
public abstract class BaseConfigRepo : IRepository<ConfigSection, string> {
    protected readonly MongoClient Client;
    protected readonly IMongoDatabase Database;
    protected IMongoCollection<ConfigSection> Collection;

    protected BaseConfigRepo(MongodbSettings settings) {
        Client = new MongoClient(settings.ConnectionString);
        Database = Client.GetDatabase(settings.DatabaseName);
        Collection = Database.GetCollection<ConfigSection>(settings.ConfigCollectionName);
    }

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
                section[keyName] = value;
                Update(section);
            }
        }
    }

    public bool HasSection(string name) {
        return GetByName(name) != null;
    }

    public ConfigSection GetByName(string name) {
        return Collection.Find(a => a.Name == name).FirstOrDefault();
    }

    public ConfigSection GetById(string id) {
        return Collection.Find(a => a.Id == id).FirstOrDefault();
    }

    public IEnumerable<ConfigSection> GetAll() {
        return Collection.Find(a => true).ToList();
    }

    public int Insert(ConfigSection obj) {
        Collection.InsertOne(obj);
        return 1;
    }

    public int Update(ConfigSection obj) {
        var result = Collection.ReplaceOne(item => item.Id == obj.Id, obj);
        return (int) result.ModifiedCount;
    }

    public int Delete(string id) {
        var result = Collection.DeleteOne(a => a.Id == id);
        return (int) result.DeletedCount;
    }
}