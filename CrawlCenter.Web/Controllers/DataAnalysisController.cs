using CrawlCenter.Contrib.WebMessages;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DataAnalysisController : Controller {
        private readonly Messages _messages;

        public DataAnalysisController(Messages messages) {
            _messages = messages;
        }

        public IActionResult Index() {
            _messages.Debug("功能未开启");
            return Redirect("/");
        }
    }
}