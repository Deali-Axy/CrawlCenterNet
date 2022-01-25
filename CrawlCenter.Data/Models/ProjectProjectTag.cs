using System;
using FreeSql.DataAnnotations;

namespace CrawlCenter.Data.Models {
    public class ProjectProjectTag {
        public string ProjectId { get; set; }

        [Navigate(nameof(ProjectId))]
        public Project Project { get; set; }

        public string ProjectTagId { get; set; }

        [Navigate(nameof(ProjectTagId))]
        public ProjectTag ProjectTag { get; set; }
    }
}