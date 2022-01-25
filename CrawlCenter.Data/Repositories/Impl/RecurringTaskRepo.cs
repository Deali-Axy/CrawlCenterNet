using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Data.Repositories.Impl {
    public class RecurringTaskRepo : BaseSqlRepo<RecurringTask> {
        public RecurringTaskRepo(IFreeSql freeSql) : base(freeSql) { }

        public override RecurringTask GetById(string id) {
            return Get(a => a.Id == id);
        }

        public override RecurringTask Get(Expression<Func<RecurringTask, bool>> expression) {
            return BaseRepo.Select.Where(expression)
                .Include(a => a.CrawlTask).ToOne();
        }

        public override IEnumerable<RecurringTask> GetAll() {
            return BaseRepo.Select.Include(a => a.CrawlTask).ToList();
        }
    }
}