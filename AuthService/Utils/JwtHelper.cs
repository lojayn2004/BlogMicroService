using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;

using AuthService.Dtos.Auth;
using AuthService.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Utils
{
    public static class JwtHelper
    {
        
        public static async Task<string> GenerateJwtTokenAsync(ApplicationUser user, 
            UserManager<ApplicationUser> _userManager,
         IOptions<JwtConfigurations> _jwtOptions)
        {
            var claims = await GetClaimsAsync(user, _userManager);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var securityToken = new JwtSecurityToken(
                claims: claims,
                issuer: _jwtOptions.Value.Issuer,
                audience: _jwtOptions.Value.Audience,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddDays(7)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        private static async Task<List<Claim>> GetClaimsAsync(ApplicationUser user, UserManager<ApplicationUser> _userManager)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim("Role", role));
            }
            return claims;
        }
    }
}
