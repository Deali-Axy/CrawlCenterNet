using System;
using System.Collections.Generic;
using AutoMapper;
using CrawlCenter.Data.Models;
using CrawlCenter.Shared.DTO.Project;
using FreeSql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Apis;

/// <summary>
/// 项目标签管理
/// </summary>
[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class ProjectTagController : ControllerBase {
    private readonly IBaseRepository<ProjectTag> _tagRepo;
    private readonly IMapper _mapper;

    public ProjectTagController(IBaseRepository<ProjectTag> tagRepo, IMapper mapper) {
        _tagRepo = tagRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<List<ProjectTag>> GetAll() {
        return _tagRepo.Select
            .Where(a => a.UserId == User.Identity.Name)
            .ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<ProjectTag> Get(string id) {
        var tag = _tagRepo.Select.Where(a => a.Id == id).ToOne();
        if (tag == null) return NotFound();
        if (tag.UserId != User.Identity?.Name) return Unauthorized(new { msg = "这不是你创建的项目标签！" });
        return tag;
    }

    [HttpPost]
    public ActionResult<ProjectTag> Create(ProjectTagCreateDto dto) {
        if (!ModelState.IsValid) return BadRequest();
        var tag = _mapper.Map<ProjectTag>(dto);
        tag.Id = Guid.NewGuid().ToString();
        return _tagRepo.Insert(tag);
    }

    [HttpPut("{id}")]
    public ActionResult<ProjectTag> Update(string id, ProjectTagEditDto dto) {
        var tag = _tagRepo.Select.Where(a => a.Id == id).ToOne();
        if (tag == null) return NotFound();
        if (tag.UserId != User.Identity?.Name) return Unauthorized(new { msg = "这不是你创建的项目标签！" });
        tag = _mapper.Map<ProjectTag>(dto);
        var ar = _tagRepo.Update(tag);
        return ar > 0 ? tag : BadRequest(new { msg = "写入数据库失败" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id) {
        var tag = _tagRepo.Select.Where(a => a.Id == id).ToOne();
        if (tag == null) return NotFound();
        if (tag.UserId != User.Identity?.Name) return Unauthorized(new { msg = "这不是你创建的项目标签！" });
        var ar = _tagRepo.Delete(tag);
        return ar > 0 ? Ok() : BadRequest(new { msg = "删除数据失败" });
    }
}