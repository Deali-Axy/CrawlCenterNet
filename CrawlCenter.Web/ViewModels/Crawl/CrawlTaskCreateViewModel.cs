﻿using System;
using System.ComponentModel.DataAnnotations;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Web.ViewModels.Crawl {
    public class CrawlTaskCreateViewModel {
        /// <summary>
        /// 爬虫名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "请输入名称，名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Display(Name = "显示名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        public string Description { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        [Display(Name = "项目")]
        public Project Project { get; set; }

        [Required(ErrorMessage = "请选择一个项目")]
        [Display(Name = "项目")]
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// 执行命令
        /// </summary>
        [Display(Name = "执行命令")]
        [Required(ErrorMessage = "命令不能为空")]
        public string Cmd { get; set; }
    }
}