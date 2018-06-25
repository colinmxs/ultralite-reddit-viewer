using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace UltraliteRedditViewer.Controllers
{
    public class AuthenticationController : Controller
    {
        private RedditSharp.AuthProvider _authProvider;
        public AuthenticationController(RedditSharp.AuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public IActionResult GetCode()
        {                      
            return Redirect(_authProvider.GetAuthUrl(Guid.NewGuid().ToString(), RedditSharp.AuthProvider.Scope.identity, true));
        }
        
        public async Task<string> GetToken(string code)
        {            
            return await _authProvider.GetOAuthTokenAsync(code);
        }
    }
}
