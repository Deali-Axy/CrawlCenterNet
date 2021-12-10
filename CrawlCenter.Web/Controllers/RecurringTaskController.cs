using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Web.ViewModels.RecurringTasks;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Controllers {
    public class RecurringTaskController : Controller {
        private readonly IAppRepository<RecurringTask> _recurringTaskRepo;

        public RecurringTaskController(IAppRepository<RecurringTask> recurringTaskRepo) {
            _recurringTaskRepo = recurringTaskRepo;
        }

        public IActionResult Index() {
            return View(new RecurringTaskIndexViewModel {
                RecurringTasks = _recurringTaskRepo.GetAll()
            });
        }
    }
}