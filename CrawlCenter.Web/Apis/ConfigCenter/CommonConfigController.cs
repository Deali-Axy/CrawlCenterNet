using System;
using System.Collections.Generic;
using System.Linq;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories.Impl;
using CrawlCenter.Shared.DTO.ConfigCenter;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Apis.ConfigCenter;

/// <summary>
/// 全局公用配置
/// </summary>
[ApiController]
[Route("Api/Config/Common")]
public class CommonConfigController : ControllerBase {
    private readonly ConfigRepo _configRepo;

    public CommonConfigController(ConfigRepo configRepo) {
        _configRepo = configRepo;
    }

    /// <summary>
    /// 获取所有节点
    /// </summary>
    /// <returns></returns>
    [HttpGet("Rest")]
    public ActionResult<List<ConfigSection>> GetAllSection() {
        return _configRepo.GetAll().ToList();
    }

    /// <summary>
    /// 获取配置节点
    /// </summary>
    /// <param name="sectionId"></param>
    /// <returns></returns>
    [HttpGet("Rest/{sectionId}")]
    public ActionResult<ConfigSection> GetSection(string sectionId) {
        var sectionObj = _configRepo[sectionId];
        if (sectionObj == null) return NotFound();
        return sectionObj;
    }

    /// <summary>
    /// 新增配置节点
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("Rest")]
    public ActionResult<ConfigSection> AddSection([FromBody] ConfigSectionCreateDto dto) {
        _configRepo[dto.Name] = new ConfigSection {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Description = dto.Description
        };
        return CreatedAtAction(nameof(AddSection), _configRepo[dto.Name]);
    }

    /// <summary>
    /// 更新配置节点
    /// </summary>
    /// <param name="section"></param>
    /// <returns></returns>
    [HttpPut("Rest")]
    public ActionResult<ConfigSection> UpdateSection([FromBody] ConfigSection section) {
        _configRepo[section.Name] = section;
        return _configRepo[section.Name];
    }

    /// <summary>
    /// 删除节点
    /// </summary>
    /// <returns></returns>
    [HttpDelete("Rest/{section}")]
    public IActionResult DeleteSection(string section) {
        var sectionObj = _configRepo.GetByName(section);
        if (sectionObj == null) return NotFound();
        var deletedCount = _configRepo.Delete(section);
        return Ok(deletedCount);
    }

    /// <summary>
    /// 获取配置键值
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpGet("Rest/{section}/{key}")]
    public ActionResult<ConfigKey> GetKey(string section, string key) {
        var configKey = _configRepo[section][key];
        if (configKey == null) return NotFound();
        return configKey;
    }

    /// <summary>
    /// 设置配置键值
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    [HttpPost("Rest/{section}/{key}")]
    public ActionResult<ConfigSection> SetKey(string section,
        string key,
        [FromForm] string value,
        [FromForm] string description = null) {
        _configRepo[section, key] = new ConfigKey {
            Name = key,
            Value = value,
            Description = description
        };
        return _configRepo[section];
    }
}