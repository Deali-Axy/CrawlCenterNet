using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Data.Repositories.Impl;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.ApiControllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase {
        private readonly ConfigRepo _configRepo;

        public ConfigController(ConfigRepo configRepo) {
            _configRepo = configRepo;
        }

        [HttpGet("[action]/{section}")]
        public ActionResult<ConfigSection> GetSection(string section) {
            var sectionObj = _configRepo[section];
            if (sectionObj == null) return NotFound();
            return sectionObj;
        }

        [HttpGet("[action]/{section}/{key}")]
        public ActionResult<ConfigKey> GetKey(string section, string key) {
            var configKey = _configRepo[section][key];
            if (configKey == null) return NotFound();
            return configKey;
        }

        [HttpPost("[action]")]
        public ActionResult<ConfigSection> SetSection([FromBody] ConfigSection section) {
            _configRepo[section.Name] = section;
            return _configRepo[section.Name];
        }

        [HttpPost("[action]/{section}/{key}")]
        public ActionResult<ConfigSection> SetKey(string section, string key, [FromForm] string value) {
            _configRepo[section, key] = new ConfigKey {
                Name = key,
                Value = value
            };
            return _configRepo[section];
        }
    }
}