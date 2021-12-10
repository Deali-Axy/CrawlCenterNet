using System;
using System.Collections.Generic;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Data.Repositories.Impl {
    public class RecurringTaskRepository : SqlRepository<RecurringTask> {
        public RecurringTaskRepository(IFreeSql freeSql) : base(freeSql) { }

        public override RecurringTask GetById(Guid id) {
            return BaseRepo.Select.Where(a => a.Id == id)
                .Include(a => a.CrawlTask).ToOne();
        }

        public override IEnumerable<RecurringTask> GetAll() {
            return BaseRepo.Select.Include(a => a.CrawlTask).ToList();
        }
    }
}