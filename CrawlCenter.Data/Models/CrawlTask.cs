using System;
using System.ComponentModel.DataAnnotations;

namespace CrawlCenter.Data.Models {
    /// <summary>
    /// 爬虫任务
    /// </summary>
    public class CrawlTask : EntityBase {
        /// <summary>
        /// 爬虫名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// 代码目录
        /// </summary>
        public string CodeDir { get; set; }

        /// <summary>
        /// 执行命令
        /// </summary>
        public string Cmd { get; set; }
    }
}