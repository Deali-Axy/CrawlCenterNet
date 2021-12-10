using System.Collections.Generic;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Web.ViewModels.Projects {
    public class ProjectCreateViewModel {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CrawlTask> CrawlTasks { get; set; }
        public ICollection<ProjectTag> ProjectTags { get; set; }
    }
}