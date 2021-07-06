using System.Collections.Generic;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Web.ViewModels.Crawl {
    public class CrawlTaskIndexViewModel {
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<CrawlTask> CrawlTasks { get; set; }
    }
}