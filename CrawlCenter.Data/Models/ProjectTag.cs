using System;
using System.Collections.Generic;

namespace CrawlCenter.Data.Models {
    public class ProjectTag : EntityBase {
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}