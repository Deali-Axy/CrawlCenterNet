using System;
using System.Collections.Generic;
using FreeSql.DataAnnotations;

namespace CrawlCenter.Data.Models {
    public class Project : EntityBase {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CrawlTask> CrawlTasks { get; set; }
        
        [Navigate(ManyToMany = typeof(ProjectProjectTag))]
        public ICollection<ProjectTag> ProjectTags { get; set; }
    }
}