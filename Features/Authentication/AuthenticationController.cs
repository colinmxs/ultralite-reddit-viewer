using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UltraliteRedditViewer.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly RedditSharp.AuthProvider _authProvider;
        private readonly IConfiguration _configuration;
        public AuthenticationController(RedditSharp.AuthProvider authProvider, IConfiguration configuration)
        {
            _authProvider = authProvider;
            _configuration = configuration;
        }

        public IActionResult GetCode()
        {                      
            return Redirect(_authProvider.GetAuthUrl(Guid.NewGuid().ToString(), RedditSharp.AuthProvider.Scope.identity, true));
        }
        
        public async Task<IActionResult> GetToken(string code)
        {
            var accessToken = await _authProvider.GetOAuthTokenAsync(code);
            var reddit = new RedditSharp.Reddit(accessToken);
            await reddit.InitOrUpdateUserAsync();
            var authenticatedUser = reddit.User.Name;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, authenticatedUser),
                new Claim("Reddit-Access-Token", accessToken)
            };

            var token = new JwtSecurityToken(
                issuer: "http://localhost:58893",
                audience: "http://localhost:58893",
                claims: claims,
                signingCredentials: creds);

            return Redirect("http://localhost:58893/auth-redirect?token=" + new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
