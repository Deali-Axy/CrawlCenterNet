using System;
using System.Collections.Generic;
using System.Linq;
using CrawlCenter.Contrib.WebMessages;
using CrawlCenter.Data;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Data.Models;
using CrawlCenter.Web.ViewModels.Crawl;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace CrawlCenter.Web.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CrawlController : Controller {
        private readonly Messages _messages;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IAppRepository<CrawlTask> _crawlTaskRepo;
        private readonly IAppRepository<Project> _projectRepo;
        private readonly IAppRepository<ProjectTag> _projectTagRepo;

        public CrawlController(Messages messages,
            IBackgroundJobClient backgroundJobClient,
            IAppRepository<CrawlTask> crawlTaskRepo,
            IAppRepository<Project> projectRepo,
            IAppRepository<ProjectTag> projectTagRepo
        ) {
            _messages = messages;
            _backgroundJobClient = backgroundJobClient;
            _crawlTaskRepo = crawlTaskRepo;
            _projectRepo = projectRepo;
            _projectTagRepo = projectTagRepo;
        }

        private List<SelectListItem> ProjectSelectList => _projectRepo.GetAll()
            .Select(a => new SelectListItem {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

        public IActionResult Index(string projectId = "", int page = 1, int pageSize = 5) {
            var crawlTasks = _crawlTaskRepo.GetAll();
            
            if (!string.IsNullOrEmpty(projectId)) {
                ViewBag.SelectedProjectName = _projectRepo.GetById(Guid.Parse(projectId)).Name;
                crawlTasks = _crawlTaskRepo.GetAll().Where(a => a.ProjectId == Guid.Parse(projectId));
            }
            
            return View(new CrawlTaskIndexViewModel {
                CrawlTasks = crawlTasks.ToPagedList(page, pageSize),
                Projects = _projectRepo.GetAll()
            });
        }
        
        public IActionResult Details(Guid id) {
            return View(_crawlTaskRepo.GetById(id));
        }

        [HttpGet]
        public IActionResult Add() {
            ViewBag.Projects = ProjectSelectList;
            return View();
        }

        [HttpPost]
        public IActionResult Add(CrawlTaskCreateViewModel viewModel) {
            ViewBag.Projects = ProjectSelectList;

            if (!ModelState.IsValid) return View();

            var newTask = new CrawlTask {
                Id = Guid.NewGuid(),
                Name = viewModel.Name,
                DisplayName = viewModel.DisplayName,
                Cmd = viewModel.Cmd,
                Description = viewModel.Description,
                ProjectId = viewModel.ProjectId
            };
            _crawlTaskRepo.Insert(newTask);
            _messages.Success("添加爬虫成功！");
            return RedirectToAction(nameof(Details), new {id = newTask.Id});
        }

        [HttpGet]
        public IActionResult Edit(Guid id) {
            ViewBag.Projects = ProjectSelectList;

            var crawlTask = _crawlTaskRepo.GetById(id);

            if (crawlTask == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return View("Error");
            }

            return View(new CrawlTaskEditViewModel {
                Id = crawlTask.Id,
                Name = crawlTask.Name,
                DisplayName = crawlTask.DisplayName,
                CodeDir = crawlTask.CodeDir,
                Cmd = crawlTask.Cmd,
                Description = crawlTask.Description,
                ProjectId = crawlTask.ProjectId
            });
        }

        [HttpPost]
        public IActionResult Edit(CrawlTaskEditViewModel model) {
            ViewBag.Projects = ProjectSelectList;
            if (!ModelState.IsValid) return View();


            var crawlTask = _crawlTaskRepo.GetById(model.Id);
            crawlTask.Name = model.Name;
            crawlTask.CodeDir = model.CodeDir;
            crawlTask.Cmd = model.Cmd;
            crawlTask.Description = model.Description;
            crawlTask.DisplayName = model.DisplayName;
            crawlTask.ProjectId = model.ProjectId;
            _crawlTaskRepo.Update(crawlTask);

            _messages.Success("更新爬虫信息成功！");
            return RedirectToAction(nameof(Details), new {id = model.Id});
        }

        public IActionResult Delete(Guid id) {
            _messages.Error("没有删除权限！");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Run(Guid id) {
            var task = _crawlTaskRepo.GetById(id);
            _backgroundJobClient.Enqueue(() => Console.WriteLine($"执行任务 {task.Name} 命令 {task.Cmd}"));
            // RecurringJob.AddOrUpdate(
            //     () => Console.WriteLine($"执行任务 {task.Name} 命令 {task.Cmd}"),
            //     Cron.Minutely);
            _messages.Info($"执行任务 {task.Name} 命令 {task.Cmd}");
            return RedirectToAction(nameof(Index));
        }
    }
}