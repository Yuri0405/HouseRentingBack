using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClientService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ClientService.Services;
public class JwtTokenHelpers
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    
    public JwtTokenHelpers(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<string> GenerateToken(AppUser user)
    {
        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securitykey,SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
    
        var userRoles = await _userManager.GetRolesAsync(user);
    
        foreach( var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
    
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires:DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
            
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}