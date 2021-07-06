using System.Collections.Generic;

namespace CrawlCenter.Data.Repositories {
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class {
        TEntity GetById(TPrimaryKey id);
        IEnumerable<TEntity> GetAll();
        int Insert(TEntity obj);
        int Update(TEntity obj);
        int Delete(TPrimaryKey id);
    }
}