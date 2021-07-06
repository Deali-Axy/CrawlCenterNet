using CrawlCenter.Data.Models;
using FreeSql;

namespace CrawlCenter.Data {
    public class AppDbContext : DbContext {
        private readonly IFreeSql _freeSql;
        public DbSet<CrawlTask> CrawlTasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTag> ProjectTags { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(IFreeSql freeSql) {
            _freeSql = freeSql;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseFreeSql(_freeSql);
        }

        protected override void OnModelCreating(ICodeFirst codefirst) {
            codefirst.SeedData();
        }
    }
}