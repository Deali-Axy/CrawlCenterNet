using System.Collections.Generic;
using AutoMapper;
using CrawlCenter.Data.Models;
using CrawlCenter.Web.ViewModels.RecurringTasks;

namespace CrawlCenter.Web.AutoMapper {
    public class RecurringTaskProfile : Profile {
        private readonly List<string> _unmapped = new List<string> {
            "CrawlTask",
        };

        public RecurringTaskProfile() {
            CreateMap<RecurringTask, RecurringTaskCreateViewModel>();
            CreateMap<RecurringTaskCreateViewModel, RecurringTask>();

            ShouldMapProperty = (property => !_unmapped.Contains(property.Name));
        }
    }
}