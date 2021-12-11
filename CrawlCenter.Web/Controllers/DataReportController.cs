using CrawlCenter.Contrib.WebMessages;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DataReportController : Controller {
        private readonly Messages _messages;

        public DataReportController(Messages messages) {
            _messages = messages;
        }
        
        public IActionResult Index() {
            _messages.Debug("功能未开启");
            return Redirect("/");
        }
    }
}