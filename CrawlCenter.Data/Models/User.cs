using System;

namespace CrawlCenter.Data.Models {
    public class User : EntityBase {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}