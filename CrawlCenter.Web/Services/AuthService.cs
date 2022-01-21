using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CrawlCenter.Data.Models;
using CrawlCenter.Data.Repositories;
using CrawlCenter.Shared.DTO;
using CrawlCenter.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CrawlCenter.Web.Services;

public class AuthService {
    private readonly SecuritySettings _secSettings;
    private readonly IAppRepository<User> _userRepo;

    public AuthService(IOptions<SecuritySettings> options,
        IAppRepository<User> userRepo) {
        _secSettings = options.Value;
        _userRepo = userRepo;
    }

    public LoginToken GenerateLoginToken(LoginUser user) {
        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Name, user.UserName),
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
        return _userRepo.Get(a => a.Name == name);
    }

    public User GetUser(ClaimsPrincipal claims) {
        var identity = (ClaimsIdentity?)claims.Identity;
        return GetUser(identity?.Name!);
    }

    public UserProfile GetUserProfile(string username) {
        var user = GetUser(username);
        return user == null
            ? null
            : new UserProfile {
                Identity = user.Id.ToString(),
                Name = user.Name,
                Phone = user.Phone,
                DateTimeJoined = user.DateTimeJoined,
                Description = ""
            };
    }

    public UserProfile GetUserProfile(ClaimsPrincipal claims) {
        var identity = (ClaimsIdentity?)claims.Identity;
        return GetUserProfile(identity?.Name!);
    }
}