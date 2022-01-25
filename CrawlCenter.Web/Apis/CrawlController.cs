using System;
using System.Collections.Generic;
using AutoMapper;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Data.Repositories.Impl;
using CrawlCenter.Shared.DTO.Crawl;
using CrawlCenter.Web.Services;
using FreeSql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Apis;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class CrawlController : ControllerBase {
    private readonly IBaseRepository<CrawlTask> _crawlRepo;
    private readonly IBaseRepository<User> _userRepo;
    private readonly AuthService _authService;
    private readonly IMapper _mapper;

    public CrawlController(IBaseRepository<CrawlTask> crawlRepo, AuthService authService, IMapper mapper, IBaseRepository<User> userRepo) {
        _crawlRepo = crawlRepo;
        _authService = authService;
        _mapper = mapper;
        _userRepo = userRepo;
    }

    /// <summary>
    /// 添加爬虫
    /// </summary>
    /// <param name="crawlDto"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<CrawlTask> Create(CrawlTaskCreateDto crawlDto) {
        var user = _authService.GetUser(HttpContext.User);

        var crawl = _mapper.Map<CrawlTask>(crawlDto);
        crawl.Id = Guid.NewGuid().ToString();
        crawl.UserId = user.Id;
        crawl.User = user;
        _crawlRepo.Insert(crawl);

        return CreatedAtAction(nameof(Create), crawl);
    }

    /// <summary>
    /// 获取全部爬虫
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<CrawlTask>> GetAll() {
        return _crawlRepo.Select
            .Where(a => a.UserId == User.Identity.Name)
            .ToList();
    }

    /// <summary>
    /// 获取爬虫
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public ActionResult<CrawlTask> Get(string id) {
        var crawl = _crawlRepo.Select.Where(a => a.Id == id).ToOne();
        if (crawl == null) return NotFound();
        return crawl;
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public ActionResult<CrawlTask> Update(CrawlTaskEditDto dto) {
        var crawl = _crawlRepo.Select.Where(a => a.Id == dto.Id).ToOne();
        if (crawl == null) return NotFound();
        if (!ModelState.IsValid) return BadRequest();
        
        var newCrawl = _mapper.Map<CrawlTask>(dto);
        var affectRows = _crawlRepo.Update(newCrawl);
        
        if (affectRows > 0) return newCrawl;
        return BadRequest(new { msg = "写入数据库失败" });
    }
}