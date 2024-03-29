﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CrawlCenter.Data.Models;
using CrawlCenter.Shared.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CrawlCenter.Data.Repositories.Impl;

/// <summary>
/// 配置中心仓库 实现基类
/// </summary>
public abstract class BaseConfigRepo : IRepository<ConfigSection, string> {
    protected readonly MongoClient Client;
    protected readonly IMongoDatabase Database;
    protected IMongoCollection<ConfigSection> Collection;
    protected MongodbSettings MongodbSettings;

    protected BaseConfigRepo(IOptions<MongodbSettings> options) {
        MongodbSettings = options.Value;
        Client = new MongoClient(MongodbSettings.ConnectionString);
        Database = Client.GetDatabase(MongodbSettings.DatabaseName);
        Collection = Database.GetCollection<ConfigSection>(MongodbSettings.ConfigCollectionName);
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

    public ConfigSection Get(Expression<Func<ConfigSection, bool>> expression) {
        return Collection.Find(expression).FirstOrDefault();
    }

    public IEnumerable<ConfigSection> GetAll() {
        return Collection.Find(a => true).ToList();
    }

    public ConfigSection Insert(ConfigSection obj) {
        Collection.InsertOne(obj);
        return obj;
    }

    public int Update(ConfigSection obj) {
        var result = Collection.ReplaceOne(item => item.Id == obj.Id, obj);
        return (int)result.ModifiedCount;
    }

    public int Delete(string id) {
        var result = Collection.DeleteOne(a => a.Id == id);
        return (int)result.DeletedCount;
    }
}