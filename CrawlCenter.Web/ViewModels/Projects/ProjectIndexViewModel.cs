using System.Collections.Generic;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Web.ViewModels.Projects {
    public class ProjectIndexViewModel {
        public IEnumerable<Project> Projects { get; set; }
    }
}