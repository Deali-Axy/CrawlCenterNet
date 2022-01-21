using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CrawlCenter.Data.Models;
using FreeSql;

namespace CrawlCenter.Data.Repositories.Impl {
    public class BaseSqlRepo<T> : IAppRepository<T> where T : EntityBase {
        public IFreeSql FreeSql { get; }

        public IBaseRepository<T> BaseRepo => FreeSql.GetRepository<T>();

        public BaseSqlRepo(IFreeSql freeSql) {
            FreeSql = freeSql;
        }

        public virtual T GetById(Guid id) {
            return FreeSql.Select<T>().Where(a => a.Id == id).First();
        }

        public virtual T Get(Expression<Func<T, bool>> expression) {
            return BaseRepo.Select.Where(expression).ToOne();
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

        /// <summary>
        /// 删除
        /// 使用 ISelect.ToDelete 实现高级删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete(Guid id) {
            // return FreeSql.Delete<T>().Where(a => a.Id == id).ExecuteAffrows();
            // ISelect.ToDelete 高级删除
            // IDelete 默认不支持导航对象，多表关联等。ISelect.ToDelete 可将查询转为 IDelete，以便使用导航对象删除数据
            // 文档：http://www.freesql.net/guide/delete.html#%E5%88%A0%E9%99%A4%E6%9D%A1%E4%BB%B6
            return BaseRepo.Select.Where(a => a.Id == id).ToDelete().ExecuteAffrows();
        }
    }
}