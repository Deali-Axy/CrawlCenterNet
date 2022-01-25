using System.Collections.Generic;
using AutoMapper;
using CrawlCenter.Data.Models;
using CrawlCenter.Shared.DTO.Crawl;
using CrawlCenter.Web.ViewModels.Crawl;
using CrawlCenter.Web.ViewModels.Projects;
using CrawlCenter.Web.ViewModels.RecurringTasks;

namespace CrawlCenter.Web.AutoMapper {
    public class CrawlTaskProfile : Profile {
        private readonly List<string> _unmapped = new List<string> {
            "Project", "User"
        };

        public CrawlTaskProfile() {
            CreateMap<CrawlTaskCreateDto, CrawlTask>();
            CreateMap<CrawlTaskCreateViewModel, CrawlTask>();
            CreateMap<CrawlTaskEditDto, CrawlTask>();
            CreateMap<CrawlTask, CrawlTaskEditViewModel>();
            CreateMap<CrawlTaskEditViewModel, CrawlTask>();

            ShouldMapProperty = (property => !_unmapped.Contains(property.Name));
        }
    }
}