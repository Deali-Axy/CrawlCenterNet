using System;
using System.Collections.Generic;
using CrawlCenter.Contrib.WebMessages;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Data.Repositories.Impl;
using CrawlCenter.Web.ViewModels.ConfigCenter;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Controllers {
    public class ConfigCenterController : Controller {
        private readonly Messages _messages;
        private readonly IRepository<ConfigSection, string> _configRepo;

        public ConfigCenterController(
            Messages messages,
            IRepository<ConfigSection, string> configRepo) {
            _messages = messages;
            _configRepo = configRepo;
        }

        public IEnumerable<ConfigSection> ConfigSections => _configRepo.GetAll();

        public IActionResult Index() {
            return View(ConfigSections);
        }

        [HttpGet]
        public IActionResult Add() {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ConfigSectionCreateViewModel model) {
            if (!ModelState.IsValid) return View();

            var configSection = new ConfigSection {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Description = model.Description
            };
            _configRepo.Insert(configSection);
            _messages.Success("添加节点成功！");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id) {
            var section = _configRepo.GetById(id);
            return View(new ConfigSectionEditViewModel {
                Id = id,
                Name = section.Name,
                Description = section.Description
            });
        }

        [HttpPost]
        public IActionResult Edit(ConfigSectionEditViewModel model) {
            if (!ModelState.IsValid) return View(model);

            var section = _configRepo.GetById(model.Id);
            section.Name = model.Name;
            section.Description = model.Description;
            _configRepo.Update(section);

            _messages.Success("更新节点信息成功！");
            return RedirectToAction(nameof(Index));
        }

        public void Test() {
            _configRepo.Insert(new ConfigSection {
                Id = Guid.NewGuid().ToString(),
                Name = "测试配置",
                Description = "反正测试就对了",
                KeyValues = new Dictionary<string, ConfigKey> {
                    {"cookie", new ConfigKey {Name = "cookie", Value = "123"}},
                    {"cookie1", new ConfigKey {Name = "cookie1", Value = "1234"}},
                    {"cookie2", new ConfigKey {Name = "cookie2", Value = "1235"}},
                    {"cookie3", new ConfigKey {Name = "cookie3", Value = "12367"}}
                }
            });
        }
    }
}