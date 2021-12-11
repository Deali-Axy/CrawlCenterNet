using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CrawlCenter.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrawlCenter.Web.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult IndexBootstrap() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }
    }
}