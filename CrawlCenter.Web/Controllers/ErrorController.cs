using System.Diagnostics;
using CrawlCenter.Web.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrawlCenter.Web.Controllers {
    public class ErrorController : Controller {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger) {
            _logger = logger;
        }


        [Route("Error/StatusCode/{statusCode:int}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult HttpStatusCodeHandler(int statusCode) {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var errorViewModel = new ErrorViewModel {
                StatusCode = statusCode,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Path = statusCodeResult.OriginalPath,
                QueryString = statusCodeResult.OriginalQueryString
            };

            return View("Error", errorViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ExceptionHandler() {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var errorViewModel = new ErrorViewModel {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            
            ViewBag.Path = exceptionHandlerPathFeature.Path;
            ViewBag.Error = exceptionHandlerPathFeature.Error;
            ViewBag.Message = exceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;
            
            _logger.LogError($"路径 {ViewBag.Path} 产生一个错误 {ViewBag.Error}");

            return View("Error", errorViewModel);
        }
    }
}