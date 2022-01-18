using System;
using System.Collections.Generic;
using System.Linq;
using CrawlCenter.Contrib.WebMessages;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Data.Repositories.Impl;
using CrawlCenter.Web.ViewModels.ConfigCenter;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
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

        public IActionResult Delete(string sectionId) {
            var section = _configRepo.GetById(sectionId);
            if (section == null) {
                _messages.Error($"节点 {sectionId} 不存在！");
                return RedirectToAction(nameof(Index));
            }

            _configRepo.Delete(sectionId);
            _messages.Success($"删除节点 {sectionId} 成功！");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AddKey(string sectionId) {
            ViewBag.SectionId = sectionId;
            return View();
        }

        [HttpPost]
        public IActionResult AddKey(string sectionId, ConfigKey configKey) {
            ViewBag.SectionId = sectionId;
            if (!ModelState.IsValid) return View();

            var section = _configRepo.GetById(sectionId);
            section.KeyValues.Add(configKey.Name, configKey);
            _configRepo.Update(section);

            _messages.Success($"在 {section.Name} 中添加新Key成功！");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateKeys(string sectionId, IEnumerable<ConfigKey> keys) {
            var form = HttpContext.Request.Form;

            var section = _configRepo.GetById(sectionId);
            section.KeyValues = keys.ToDictionary(key => key.Name);
            _configRepo.Update(section);
            _messages.Success($"更新 {section.Name} 的Key列表成功！");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteKey(string sectionId, string keyId) {
            var section = _configRepo.GetById(sectionId);
            if (section == null) {
                _messages.Error($"节点 {sectionId} 不存在！");
                return RedirectToAction(nameof(Index));
            }

            if (!section.KeyValues.ContainsKey(keyId)) {
                _messages.Error($"节点 {sectionId} 不存在ID为 {keyId} 的Key！");
                return RedirectToAction(nameof(Index));
            }

            section.KeyValues.Remove(keyId);
            _configRepo.Update(section);
            _messages.Success($"删除节点 {sectionId} 下的 {keyId} key成功！");

            return RedirectToAction(nameof(Index));
        }
    }
}