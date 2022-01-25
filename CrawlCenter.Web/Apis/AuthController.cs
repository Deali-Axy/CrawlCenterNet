using System;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Shared.DTO.User;
using CrawlCenter.Shared.Models;
using CrawlCenter.Web.Services;
using Masuit.Tools.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrawlCenter.Web.Apis;

[ApiController]
[Route("Api/[controller]/[action]")]
public class AuthController : ControllerBase {
    private readonly AuthService _authService;
    private readonly IAppRepository<User> _userRepo;

    public AuthController(AuthService authService, IAppRepository<User> userRepo) {
        _authService = authService;
        _userRepo = userRepo;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="loginUser"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<LoginToken> Login(LoginUser loginUser) {
        var user = _authService.GetUser(loginUser.Username);
        if (user == null) return NotFound();

        var md5Pwd = loginUser.Password.MDString();
        if (md5Pwd != user.Password) return Unauthorized();

        return _authService.GenerateLoginToken(user);
    }

    /// <summary>
    /// 用户注册
    /// </summary>
    /// <param name="userCreateDto"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<User> SignUp(UserCreateDto userCreateDto) {
        if (_userRepo.Get(a => a.Name == userCreateDto.Name) != null)
            return BadRequest(new { msg = "用户已存在！" });
        var user = new User {
            Id = Guid.NewGuid().ToString(),
            Name = userCreateDto.Name,
            Password = userCreateDto.Password.MDString(),
            DateTimeJoined = DateTime.Now,
            Phone = userCreateDto.Phone
        };

        if (_userRepo.Insert(user) != null) return user;

        return BadRequest(new { msg = "新用户插入数据库失败！" });
    }

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public ActionResult<User> GetUser() {
        var user = _authService.GetUser(User);
        if (user == null) return NotFound();
        return user;
    }
}