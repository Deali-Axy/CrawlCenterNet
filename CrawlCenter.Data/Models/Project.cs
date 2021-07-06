using System;
using System.Collections.Generic;

namespace CrawlCenter.Data.Models {
    public class Project : EntityBase {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CrawlTask> CrawlTasks { get; set; }
        public ICollection<ProjectTag> ProjectTags { get; set; }
    }
}