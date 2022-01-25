using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

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
        public string? ProjectId { get; set; }

        /// <summary>
        /// 代码目录
        /// </summary>
        public string CodeDir { get; set; }

        /// <summary>
        /// 执行命令
        /// </summary>
        public string Cmd { get; set; }
        
        /// <summary>
        /// 创建这个爬虫的用户
        /// </summary>
        public User User { get; set; }
        
        /// <summary>
        /// 用户ID
        /// </summary>
        public string? UserId { get; set; }
    }
}