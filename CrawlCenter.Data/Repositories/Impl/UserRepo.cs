using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Data.Repositories.Impl;

public class UserRepo : BaseSqlRepo<User> {
    public UserRepo(IFreeSql freeSql) : base(freeSql) {
    }

    public override User Get(Expression<Func<User, bool>> expression) {
        return BaseRepo.Select.Where(expression)
            .IncludeMany(a => a.CrawlTasks)
            .ToOne();
    }

    public override IEnumerable<User> GetAll() {
        return BaseRepo.Select.IncludeMany(a => a.CrawlTasks).ToList();
    }
}