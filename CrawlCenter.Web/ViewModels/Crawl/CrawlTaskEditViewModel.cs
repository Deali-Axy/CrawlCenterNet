﻿using System;
using System.ComponentModel.DataAnnotations;
using CrawlCenter.Data.Models;

namespace CrawlCenter.Web.ViewModels.Crawl {
    public class CrawlTaskEditViewModel : CrawlTaskCreateViewModel {
        public Guid Id { get; set; }
    }
}