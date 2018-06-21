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
        private IConfiguration _config;

        public RedditController(RedditSharp.RefreshTokenWebAgentPool webAgentPool, IConfiguration config)
        {
            _webAgentPool = webAgentPool;
            _config = config;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            var userName = _config["TestUN"];
            var password = _config["TestPW"];
            var clientId = _config["RedditClientID"];
            
            var webAgent = await _webAgentPool.GetOrCreateWebAgentAsync(userName, async (uname, uagent, rlimit) =>
            {
                var clientSecret = _config["RedditClientSecret"];
                var redirectUri = _config["RedditRedirectURI"];
                var userManager = new RedditSharp.AuthProvider(clientId, clientSecret, redirectUri);

                string accessToken = await userManager.GetOAuthTokenAsync(userName, password);
                uname = userName; //for webagent being created
                return new RedditSharp.RefreshTokenPoolEntry(uname, accessToken, RedditSharp.RateLimitMode.Burst);
            });

            var reddit = new RedditSharp.Reddit(webAgent, true);
            var result = "";
            var posts = reddit.RSlashAll.GetPosts(1).ForEachAsync(post =>
            {
                result = post.SelfText;
            });
            return result;
        }
    }
}
