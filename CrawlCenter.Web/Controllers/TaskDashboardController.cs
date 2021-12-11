using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TaskDashboardController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}