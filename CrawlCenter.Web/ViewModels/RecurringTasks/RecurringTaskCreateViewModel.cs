using System;
using System.ComponentModel.DataAnnotations;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Web.ViewModels.RecurringTasks {
    public class RecurringTaskCreateViewModel {
        /// <summary>
        /// 定时任务名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "请输入名称，名称不能为空")]
        public string Name { get; set; }
        
        /// <summary>
        /// 爬虫
        /// </summary>
        [Display(Name = "爬虫")]
        public CrawlTask CrawlTask { get; set; }

        /// <summary>
        /// 爬虫ID
        /// </summary>
        public Guid? CrawlTaskId { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        [Display(Name = "Cron表达式")]
        public string Cron { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [Display(Name = "参数")]
        public string Parameters { get; set; }

        /// <summary>
        /// 定时任务描述
        /// </summary>
        [Display(Name = "描述")]
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}