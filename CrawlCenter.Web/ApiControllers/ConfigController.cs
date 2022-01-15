using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Data.Repositories.Impl;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.ApiControllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase {
        private readonly IRepository<ConfigSection, string> _configRepo;

        public ConfigController(IRepository<ConfigSection, string> configRepo) {
            _configRepo = configRepo;
        }
    }
}