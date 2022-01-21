using System;

namespace CrawlCenter.Data.Models; 

public class UserCrawlTask : EntityBase {
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid CrawlTaskId { get; set; }
    public CrawlTask CrawlTask { get; set; }
}