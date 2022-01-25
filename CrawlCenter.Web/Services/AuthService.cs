using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Shared.DTO;
using CrawlCenter.Shared.Models;
using FreeSql;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CrawlCenter.Web.Services;

public class AuthService {
    private readonly SecuritySettings _secSettings;
    private readonly IBaseRepository<User> _userRepo;

    public AuthService(IOptions<SecuritySettings> options,
        IBaseRepository<User> userRepo) {
        _secSettings = options.Value;
        _userRepo = userRepo;
    }

    public LoginToken GenerateLoginToken(User user) {
        var claims = new List<Claim> {
            new("username", user.Name),
            new(JwtRegisteredClaimNames.Name, user.Id), // User.Identity.Name
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secSettings.Token.Key));
        var signCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwtToken = new JwtSecurityToken(
            issuer: _secSettings.Token.Issuer,
            audience: _secSettings.Token.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: signCredential);

        return new LoginToken {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            Expiration = TimeZoneInfo.ConvertTimeFromUtc(jwtToken.ValidTo, TimeZoneInfo.Local)
        };
    }

    public User GetUser(string name) {
        return _userRepo.Select.Where(a => a.Name == name).ToOne();
    }

    public User GetUser(ClaimsPrincipal user) {
        return GetUser(user.Identity?.Name);
    }

    /// <summary>
    /// 从 JwtToken 的 claims 里获取 UserProfile 对象
    /// （无数据库请求）
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public UserProfile GetUserProfile(ClaimsPrincipal user) {
        DateTime.TryParse(
            user.Claims.FirstOrDefault(c => c.Type == "datetime_joined")?.Value!,
            out var dateTimeJoined);

        return new UserProfile {
            Identity = user.Identity?.Name,
            Name = user.Claims.FirstOrDefault(c => c.Type == "username")?.Value,
            Phone = user.Claims.FirstOrDefault(c => c.Type == "phone")?.Value,
            DateTimeJoined = dateTimeJoined,
            Description = user.Claims.FirstOrDefault(c => c.Type == "description")?.Value,
        };
    }
}