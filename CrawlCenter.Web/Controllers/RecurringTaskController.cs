using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CrawlCenter.Contrib.WebMessages;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Web.Tasks;
using CrawlCenter.Web.ViewModels.RecurringTasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrawlCenter.Web.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RecurringTaskController : Controller {
        private readonly Messages _messages;
        private readonly IMapper _mapper;
        private readonly IAppRepository<CrawlTask> _crawlTaskRepo;
        private readonly IAppRepository<RecurringTask> _recurringTaskRepo;

        public RecurringTaskController(Messages messages,
            IMapper mapper,
            IAppRepository<CrawlTask> crawlTaskRepo,
            IAppRepository<RecurringTask> recurringTaskRepo) {
            _messages = messages;
            _mapper = mapper;
            _crawlTaskRepo = crawlTaskRepo;
            _recurringTaskRepo = recurringTaskRepo;
        }

        public List<SelectListItem> CrawlSelectList => _crawlTaskRepo.GetAll()
            .Select(item => new SelectListItem {
                Value = item.Id.ToString(),
                Text = $"[{item.Project.Name}] {item.DisplayName}"
            }).ToList();

        public IActionResult Index() {
            return View(new RecurringTaskIndexViewModel {
                RecurringTasks = _recurringTaskRepo.GetAll()
            });
        }

        public IActionResult Details(Guid id) {
            return View(_recurringTaskRepo.GetById(id));
        }

        [HttpGet]
        public IActionResult Add() {
            ViewBag.CrawlTasks = CrawlSelectList;
            return View();
        }

        [HttpPost]
        public IActionResult Add(RecurringTaskCreateViewModel viewModel) {
            ViewBag.CrawlTasks = CrawlSelectList;
            if (!ModelState.IsValid) return View();

            var recurringTask = _mapper.Map<RecurringTask>(viewModel);
            recurringTask.Id = Guid.NewGuid();
            _recurringTaskRepo.Insert(recurringTask);
            _messages.Success("添加定时任务成功！");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(Guid id) {
            ViewBag.CrawlTasks = CrawlSelectList;
            var task = _recurringTaskRepo.GetById(id);

            if (task == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return View("Error");
            }

            return View(_mapper.Map<RecurringTaskEditViewModel>(task));
        }

        [HttpPost]
        public IActionResult Edit(RecurringTaskEditViewModel viewModel) {
            ViewBag.CrawlTasks = CrawlSelectList;
            if (!ModelState.IsValid) return View();

            var task = _mapper.Map<RecurringTask>(viewModel);
            var affectRows = _recurringTaskRepo.Update(task);
            if (affectRows > 0)
                _messages.Success("修改定时任务成功！");
            else
                _messages.Error("修改定时任务失败！");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete([FromForm] Guid id) {
            var job = _recurringTaskRepo.GetById(id);
            if (job == null) return NotFound();
            RecurringJob.RemoveIfExists(job.Id.ToString());

            var affectRows = _recurringTaskRepo.Delete(id);
            if (affectRows > 0)
                _messages.Success($"删除定时任务 {id} 成功！");
            else
                _messages.Error($"删除定时任务 {id} 失败！");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult SyncData() {
            var jobs = _recurringTaskRepo.GetAll().ToList();
            foreach (var job in jobs) {
                RecurringJob.RemoveIfExists(job.Id.ToString());
                RecurringJob.AddOrUpdate(job.Id.ToString(),
                    () => new RunCrawl().Run(job.CrawlTask),
                    () => job.Cron);
            }

            _messages.Info($"已同步{jobs.Count}个定时任务！");

            return RedirectToAction(nameof(Index));
        }
    }
}