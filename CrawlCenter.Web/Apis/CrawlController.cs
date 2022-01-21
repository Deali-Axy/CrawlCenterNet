using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Apis; 

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class CrawlController : ControllerBase {
    private readonly IAppRepository<CrawlTask> _crawlRepo;
    private readonly AuthService _authService;

    public CrawlController(IAppRepository<CrawlTask> crawlRepo, AuthService authService) {
        _crawlRepo = crawlRepo;
        _authService = authService;
    }
}