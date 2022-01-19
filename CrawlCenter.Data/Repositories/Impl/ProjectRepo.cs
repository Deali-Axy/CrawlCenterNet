using System;
using System.Collections.Generic;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Data.Repositories.Impl {
    public class ProjectRepo : BaseSqlRepo<Project> {
        public ProjectRepo(IFreeSql freeSql) : base(freeSql) { }

        public override Project GetById(Guid id) {
            return BaseRepo.Select.Where(a => a.Id == id)
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