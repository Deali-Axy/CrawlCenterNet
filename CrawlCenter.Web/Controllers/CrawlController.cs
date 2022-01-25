using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using CrawlCenter.Contrib.WebMessages;
using CrawlCenter.Data;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories.Impl;
using CrawlCenter.Web.Tasks;
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
        private readonly IMapper _mapper;
        private readonly Messages _messages;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IAppRepository<CrawlTask> _crawlTaskRepo;
        private readonly IAppRepository<Project> _projectRepo;
        private readonly IAppRepository<ProjectTag> _projectTagRepo;

        public CrawlController(IMapper mapper,
            Messages messages,
            IBackgroundJobClient backgroundJobClient,
            IAppRepository<CrawlTask> crawlTaskRepo,
            IAppRepository<Project> projectRepo,
            IAppRepository<ProjectTag> projectTagRepo
        ) {
            _mapper = mapper;
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
                ViewBag.SelectedProjectName = _projectRepo.GetById(projectId).Name;
                crawlTasks = _crawlTaskRepo.GetAll().Where(a => a.ProjectId == projectId);
            }

            return View(new CrawlTaskIndexViewModel {
                CrawlTasks = crawlTasks.ToPagedList(page, pageSize),
                Projects = _projectRepo.GetAll()
            });
        }

        public IActionResult Details(string id) {
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

            var newTask = _mapper.Map<CrawlTask>(viewModel);
            newTask.Id = Guid.NewGuid().ToString();
            _crawlTaskRepo.Insert(newTask);
            _messages.Success("添加爬虫成功！");
            
            return RedirectToAction(nameof(Details), new { id = newTask.Id });
        }

        [HttpGet]
        public IActionResult Edit(string id) {
            ViewBag.Projects = ProjectSelectList;

            var crawlTask = _crawlTaskRepo.GetById(id);

            if (crawlTask == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return View("Error");
            }

            return View(_mapper.Map<CrawlTaskEditViewModel>(crawlTask));
        }

        [HttpPost]
        public IActionResult Edit(CrawlTaskEditViewModel model) {
            ViewBag.Projects = ProjectSelectList;
            if (!ModelState.IsValid) return View();
            
            _crawlTaskRepo.Update(_mapper.Map<CrawlTask>(model));

            _messages.Success("更新爬虫信息成功！");
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpPost]
        public IActionResult Delete([FromForm] string id, [FromServices] IAppRepository<RecurringTask> recurringTaskRepo) {
            var recurringTaskDelete = ((RecurringTaskRepo)recurringTaskRepo).BaseRepo
                .Where(a => a.CrawlTaskId == id).ToDelete();
            var affectRows = _crawlTaskRepo.Delete(id);
            if (affectRows > 0) {
                var affectRecurringTasks = recurringTaskDelete.ExecuteAffrows();
                _messages.Success($"删除爬虫 {id} 成功，已删除关联的{affectRecurringTasks}个定时任务。");
            }
            else
                _messages.Error($"删除爬虫 {id} 失败！");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Run(string id) {
            var task = _crawlTaskRepo.GetById(id);
            _backgroundJobClient.Enqueue(() => new RunCrawl().Run(task));
            // RecurringJob.AddOrUpdate(
            //     () => Console.WriteLine($"执行任务 {task.Name} 命令 {task.Cmd}"),
            //     Cron.Minutely);
            _messages.Info($"执行任务 {task.Name} 命令 {task.Cmd}");
            return RedirectToAction(nameof(Details), new { id = task.Id });
        }
    }
}