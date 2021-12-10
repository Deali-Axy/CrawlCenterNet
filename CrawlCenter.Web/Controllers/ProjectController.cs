using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Web.ViewModels.Projects;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Controllers {
    public class ProjectController : Controller {
        private readonly IAppRepository<Project> _projectRepo;

        public ProjectController(IAppRepository<Project> projectRepo) {
            _projectRepo = projectRepo;
        }

        public IActionResult Index() {
            var projects = _projectRepo.GetAll();

            return View(new ProjectIndexViewModel {
                Projects = projects
            });
        }
    }
}