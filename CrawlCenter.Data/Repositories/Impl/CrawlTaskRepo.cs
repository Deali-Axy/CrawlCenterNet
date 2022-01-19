using System;
using System.Collections.Generic;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Data.Repositories.Impl {
    public class CrawlTaskRepo : BaseSqlRepo<CrawlTask> {
        public CrawlTaskRepo(IFreeSql freeSql) : base(freeSql) { }

        public override CrawlTask GetById(Guid id) {
            return BaseRepo.Select.Where(a => a.Id == id)
                .Include(a => a.Project).First();
        }

        public override IEnumerable<CrawlTask> GetAll() {
            return BaseRepo.Select.Include(a => a.Project).ToList();
        }
    }
}