using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Controllers {
    public class TaskDashboardController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}