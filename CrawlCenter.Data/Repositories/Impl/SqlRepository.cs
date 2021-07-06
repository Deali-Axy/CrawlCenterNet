using System;
using System.Collections.Generic;
using CrawlCenter.Data.Models;
using FreeSql;

namespace CrawlCenter.Data.Repositories.Impl {
    public class SqlRepository<T> : IAppRepository<T> where T : EntityBase {
        protected IFreeSql FreeSql { get; }

        protected IBaseRepository<T> BaseRepo => FreeSql.GetRepository<T>();

        public SqlRepository(IFreeSql freeSql) {
            FreeSql = freeSql;
        }

        public virtual T GetById(Guid id) {
            return FreeSql.Select<T>().Where(a => a.Id == id).First();
        }

        public virtual IEnumerable<T> GetAll() {
            return FreeSql.Select<T>().ToList();
        }

        public virtual int Insert(T obj) {
            return FreeSql.InsertOrUpdate<T>()
                .SetSource(obj) //需要操作的数据
                .IfExistsDoNothing() //如果数据存在，啥事也不干（相当于只有不存在数据时才插入）
                .ExecuteAffrows();
        }

        public virtual int Update(T obj) {
            return BaseRepo.Update(obj);
        }

        public virtual int Delete(Guid id) {
            return FreeSql.Delete<T>().Where(a => a.Id == id).ExecuteAffrows();
        }
    }
}