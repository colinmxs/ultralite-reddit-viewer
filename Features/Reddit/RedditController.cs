using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace UltraliteRedditViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedditController : ControllerBase
    {
        private RedditSharp.RefreshTokenWebAgentPool _webAgentPool;
        private RedditSharp.AuthProvider _authProvider;

        public RedditController(RedditSharp.RefreshTokenWebAgentPool webAgentPool, RedditSharp.AuthProvider authProvider)
        {
            _webAgentPool = webAgentPool;
            _authProvider = authProvider;
        }

        [Route("me")]        
        [HttpGet]
        public async Task<object> Me()
        {
            object me = null;
            //get token from header
            var token = HttpContext.Request.Headers.Single(h => h.Key == "Authorization").Value.ToString().Remove(0, 7);
            var reddit = new RedditSharp.Reddit(token);
            await reddit.InitOrUpdateUserAsync();
            return reddit.User;
        }

        [Route("popular")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<object> Popular(int pageSize)
        {
            //var userName = "colinmxs";            
            //var webAgent = await _webAgentPool.GetOrCreateWebAgentAsync(userName, (uname, uagent, rlimit) =>
            //{

            //    uname = userName;
            //    return Task.FromResult(new RedditSharp.RefreshTokenPoolEntry(uname, accessToken, RedditSharp.RateLimitMode.Burst));
            //});
            var result = "";
            var reddit = new RedditSharp.Reddit(); await reddit.InitOrUpdateUserAsync();
            //await reddit.RSlashAll.GetPosts(3).ForEachAsync(post => result += post.Title);
            return result;
        }
    }
}
