using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UltraliteRedditViewer.Infrastructure;

namespace UltraliteRedditViewer.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly RedditAuthProvider _authProvider;
        private readonly IConfiguration _configuration;        

        public AuthenticationController(RedditAuthProvider authProvider, IConfiguration configuration)
        {
            _authProvider = authProvider;
            _configuration = configuration;
        }

        public IActionResult GetCode()
        {                      
            return Redirect(_authProvider.GetAuthUrl(Guid.NewGuid().ToString(), "identity,mysubreddits,read", true));
        }
        
        public async Task<IActionResult> GetToken(string code)
        {
                        
            var redditTokens = await _authProvider.GetTokenAsync(code, false);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var webAgent = new RedditSharp.WebAgent(redditTokens.AccessToken, null, $"dotnet:UVR:v0.0.1 (fetching username...)");
            var reddit = new RedditSharp.Reddit(webAgent, true);                        
            var name = reddit.User.Name;
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(nameof(redditTokens.AccessToken), redditTokens.AccessToken),
                new Claim(nameof(redditTokens.RefreshToken), redditTokens.RefreshToken),
                new Claim(nameof(redditTokens.ExpiresAt), redditTokens.ExpiresAt.ToString()),
                new Claim(nameof(redditTokens.TokenType), redditTokens.TokenType),
            };

            var token = new JwtSecurityToken(
                issuer: "http://localhost:58893",
                audience: "http://localhost:58893",
                claims: claims,
                signingCredentials: creds);           

            return Redirect($"http://localhost:58893/auth-redirect?token={new JwtSecurityTokenHandler().WriteToken(token)}");
        }
    }
}
