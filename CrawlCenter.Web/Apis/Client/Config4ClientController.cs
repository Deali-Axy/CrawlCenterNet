using System;
using System.Collections.Generic;
using System.Linq;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories.Impl;
using CrawlCenter.Shared.DTO.ConfigCenter;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Apis.Client;

/// <summary>
/// 配置中心（爬虫客户端接口）
/// </summary>
[ApiController]
[Route("Api/Client/Config")]
public class Config4ClientController : ControllerBase {
    private readonly ConfigRepo _configRepo;

    public Config4ClientController(ConfigRepo configRepo) {
        _configRepo = configRepo;
    }

    /// <summary>
    /// 获取配置节点
    /// </summary>
    /// <param name="sectionName"></param>
    /// <returns></returns>
    [HttpGet("{sectionName}")]
    public ActionResult<ConfigSection> GetSection(string sectionName) {
        var sectionObj = _configRepo[sectionName];
        if (sectionObj == null) return NotFound();
        return sectionObj;
    }

    /// <summary>
    /// 获取配置键值
    /// </summary>
    /// <param name="sectionName"></param>
    /// <param name="keyName"></param>
    /// <returns></returns>
    [HttpGet("{sectionName}/{keyName}")]
    public ActionResult<ConfigKey> GetKey(string sectionName, string keyName) {
        var configKey = _configRepo[sectionName][keyName];
        if (configKey == null) return NotFound();
        return configKey;
    }

    /// <summary>
    /// 设置配置键值
    /// </summary>
    /// <param name="sectionName"></param>
    /// <param name="keyName"></param>
    /// <param name="value"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    [HttpPost("{sectionName}/{keyName}")]
    public ActionResult<ConfigSection> SetKey(string sectionName,
        string keyName,
        [FromForm] string value,
        [FromForm] string description = null) {
        _configRepo[sectionName, keyName] = new ConfigKey {
            Name = keyName,
            Value = value,
            Description = description
        };
        return _configRepo[sectionName];
    }
}