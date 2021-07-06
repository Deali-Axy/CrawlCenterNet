using System;
using System.Collections.Generic;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Data.Repositories {
    public interface IAppRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : class {
    }
}