using System;

namespace CrawlCenter.Data.Models {
    /// <summary>
    /// 定时任务
    /// </summary>
    public class RecurringTask : EntityBase {
        /// <summary>
        /// 定时任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 爬虫
        /// </summary>
        public CrawlTask CrawlTask { get; set; }

        /// <summary>
        /// 爬虫ID
        /// </summary>
        public Guid? CrawlTaskId { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 定时任务描述
        /// </summary>
        public string Description { get; set; }
    }
}