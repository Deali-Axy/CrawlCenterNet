using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CrawlCenter.Contrib.WebMessages;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Web.ViewModels.RecurringTasks;
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
    }
}