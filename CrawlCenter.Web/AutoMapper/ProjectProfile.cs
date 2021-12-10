using System.Collections.Generic;
using AutoMapper;
using CrawlCenter.Data.Models;
using CrawlCenter.Web.ViewModels.Projects;
using CrawlCenter.Web.ViewModels.RecurringTasks;

namespace CrawlCenter.Web.AutoMapper {
    public class ProjectProfile : Profile {
        private readonly List<string> _unmapped = new List<string> {
            "CrawlTasks", "ProjectTags"
        };

        public ProjectProfile() {
            CreateMap<Project, ProjectCreateViewModel>();
            CreateMap<ProjectCreateViewModel, Project>();

            ShouldMapProperty = (property => !_unmapped.Contains(property.Name));
        }
    }
}