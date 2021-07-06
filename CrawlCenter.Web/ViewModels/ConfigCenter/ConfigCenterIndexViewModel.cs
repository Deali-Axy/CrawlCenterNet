using System.Collections.Generic;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Web.ViewModels.ConfigCenter {
    public class ConfigCenterIndexViewModel {
        public IEnumerable<ConfigSection> ConfigSections { get; set; }
    }
}