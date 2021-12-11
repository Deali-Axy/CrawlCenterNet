using System;
using System.Collections.Generic;
using FreeSql.DataAnnotations;

namespace CrawlCenter.Data.Models {
    public class ProjectTag : EntityBase {
        public string Name { get; set; }
        
        [Navigate(ManyToMany = typeof(ProjectProjectTag))]
        public ICollection<Project> Projects { get; set; }
    }
}