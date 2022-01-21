using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Data.Repositories.Impl {
    public class ProjectRepo : BaseSqlRepo<Project> {
        public ProjectRepo(IFreeSql freeSql) : base(freeSql) { }

        public override Project GetById(Guid id) {
            return Get(a => a.Id == id);
        }

        public override Project Get(Expression<Func<Project, bool>> expression) {
            return BaseRepo.Select.Where(expression)
                .IncludeMany(a => a.CrawlTasks)
                .IncludeMany(a => a.ProjectTags)
                .ToOne();
        }

        public override IEnumerable<Project> GetAll() {
            return BaseRepo.Select
                .IncludeMany(a => a.CrawlTasks)
                .IncludeMany(a => a.ProjectTags)
                .ToList();
        }
    }
}