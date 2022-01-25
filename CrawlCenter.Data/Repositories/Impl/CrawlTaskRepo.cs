using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Data.Repositories.Impl {
    public class CrawlTaskRepo : BaseSqlRepo<CrawlTask> {
        public CrawlTaskRepo(IFreeSql freeSql) : base(freeSql) { }

        public override CrawlTask GetById(string id) {
            return Get(a => a.Id == id);
        }

        public override CrawlTask Get(Expression<Func<CrawlTask, bool>> expression) {
            return BaseRepo.Select.Where(expression).Include(a => a.Project).First();
        }

        public override IEnumerable<CrawlTask> GetAll() {
            return BaseRepo.Select.Include(a => a.Project).ToList();
        }
    }
}