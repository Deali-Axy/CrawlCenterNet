using System.Collections.Generic;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Web.ViewModels.RecurringTasks {
    public class RecurringTaskIndexViewModel {
        public IEnumerable<RecurringTask> RecurringTasks { get; set; }
    }
}