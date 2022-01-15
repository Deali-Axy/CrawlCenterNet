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

        public string Get(string section, string key) {
            try {
                return _configRepo.GetByName(section).KeyValues[key].Value;
            }
            catch (Exception ex) {
                ex.ToExceptionless().Submit();
                return null;
            }
        }

        public void Set(string section, string key, string value) {
            try {
                ConfigSection cfgSection = null;
                cfgSection = _configRepo.GetByName(section);
                if (cfgSection == null) {
                    cfgSection = new ConfigSection { Name = section };
                    _configRepo.Insert(cfgSection);
                } 
                if (cfgSection.KeyValues.ContainsKey(key)) {
                    var cfgKey = cfgSection.KeyValues[key];
                    cfgKey.Value = value;
                }
                else
                    cfgSection.KeyValues.Add(key, new ConfigKey { Name = key, Value = value });
            }
            catch (Exception ex) {
                ex.ToExceptionless().Submit();
            }
        }
    }
}