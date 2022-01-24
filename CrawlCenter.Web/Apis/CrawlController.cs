using System;
using AutoMapper;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Shared.DTO.Crawl;
using CrawlCenter.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Apis;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class CrawlController : ControllerBase {
    private readonly IAppRepository<CrawlTask> _crawlRepo;
    private readonly IAppRepository<User> _userRepo;
    private readonly AuthService _authService;
    private readonly IMapper _mapper;

    public CrawlController(IAppRepository<CrawlTask> crawlRepo, AuthService authService, IMapper mapper, IAppRepository<User> userRepo) {
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
        var crawl = _mapper.Map<CrawlTask>(crawlDto);
        crawl.Id = Guid.NewGuid();
        _crawlRepo.Insert(crawl);
        
        var user = _authService.GetUser(HttpContext.User);
        user.CrawlTasks.Add(crawl);
        _userRepo.Update(user);
        
        return CreatedAtAction(nameof(Create), crawl);
    }
}