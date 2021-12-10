using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CrawlCenter.Contrib.WebMessages;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Web.ViewModels.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrawlCenter.Web.Controllers {
    public class ProjectController : Controller {
        private readonly Messages _messages;
        private readonly IMapper _mapper;
        private readonly IAppRepository<Project> _projectRepo;
        private readonly IAppRepository<ProjectTag> _projectTagRepo;

        public ProjectController(Messages messages,
            IMapper mapper,
            IAppRepository<Project> projectRepo,
            IAppRepository<ProjectTag> projectTagRepo) {
            _messages = messages;
            _mapper = mapper;
            _projectRepo = projectRepo;
            _projectTagRepo = projectTagRepo;
        }

        public List<SelectListItem> ProjectTagSelectList => _projectTagRepo.GetAll()
            .Select(a => new SelectListItem {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();


        public IActionResult Index() {
            var projects = _projectRepo.GetAll();

            return View(new ProjectIndexViewModel {
                Projects = projects
            });
        }

        [HttpGet]
        public IActionResult Add() {
            ViewBag.ProjectTags = ProjectTagSelectList;
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProjectCreateViewModel viewModel) {
            ViewBag.ProjectTags = ProjectTagSelectList;
            if (!ModelState.IsValid) return View();

            var project = _mapper.Map<Project>(viewModel);
            project.Id = Guid.NewGuid();
            _projectRepo.Insert(project);
            _messages.Success("添加项目成功！");

            return RedirectToAction(nameof(Index));
        }
    }
}