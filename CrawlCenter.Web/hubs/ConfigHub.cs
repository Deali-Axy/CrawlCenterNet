using System;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Data.Repositories.Impl;
using Exceptionless;
using Microsoft.AspNetCore.SignalR;

namespace CrawlCenter.Web.hubs {
    public class ConfigHub : Hub {
        private readonly ConfigRepo _configRepo;

        public ConfigHub(ConfigRepo configRepo) {
            _configRepo = configRepo;
        }

        public ConfigKey Get(string section, string key) {
            try {
                return _configRepo[section][key];
            }
            catch (Exception ex) {
                ex.ToExceptionless().Submit();
                return null;
            }
        }

        public void Set(string sectionName, string key, string value) {
            try {
                _configRepo[sectionName][key] = new ConfigKey {
                    Name = key,
                    Value = value
                };
            }
            catch (Exception ex) {
                ex.ToExceptionless().Submit();
            }
        }
    }
}