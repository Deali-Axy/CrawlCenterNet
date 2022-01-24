using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CrawlCenter.Data.Repositories {
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class {
        TEntity GetById(TPrimaryKey id);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> GetAll();
        TEntity Insert(TEntity obj);
        int Update(TEntity obj);
        int Delete(TPrimaryKey id);
    }
}