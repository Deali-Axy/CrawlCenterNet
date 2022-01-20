using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CrawlCenter.Shared.DTO;
using CrawlCenter.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CrawlCenter.Web.Services; 

public class AuthService {
    private readonly SecuritySettings _secSettings;
    
    public AuthService(IOptions<SecuritySettings> options) {
        _secSettings = options.Value;
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
}