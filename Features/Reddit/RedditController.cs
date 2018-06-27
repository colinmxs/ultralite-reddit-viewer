using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RedditSharp.Things;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraliteRedditViewer.Infrastructure;

namespace UltraliteRedditViewer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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

        [Route("me")]        
        [HttpGet]
        public async Task<object> Me()
        {
            var userName = User.Identity.Name;
            var webAgent = await _webAgentPool.GetOrCreateWebAgentAsync(userName, (name, agent, limit) =>
            {
                string refreshToken = User.GetRedditAuthTokens().RefreshToken;
                name = userName;
                agent = $"dotnet:UVR:v0.0.1 (by /u/{userName})";
                limit = RedditSharp.RateLimitMode.Burst;
                return Task.FromResult(new RedditSharp.RefreshTokenPoolEntry(name, refreshToken, limit, agent));
            });

            var reddit = new RedditSharp.Reddit(webAgent, true);
            var listing = reddit.User.GetSubscribedSubreddits();
            var newestPosts = new List<string>();
            listing.ForEach(async sr =>
            {
                var newestPost = await sr.GetPosts(RedditSharp.Things.Subreddit.Sort.New, 1)
                .First()
                .ConfigureAwait(false);
                newestPosts.Add(newestPost.Title);
            });
            return newestPosts;
        }

        [Route("popular")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 360, Location = ResponseCacheLocation.Any)]
        public async Task<object> Popular(int pageSize)
        {
            var webAgent = new RedditSharp.BotWebAgent(_config["PopbotUN"], _config["PopbotPW"], _config["RedditClientID-Popbot"], _config["RedditClientSecret-Popbot"], _config["RedditRedirectURI-Popbot"]);
            var reddit = new RedditSharp.Reddit(webAgent, false);
            var posts = new List<Post>();
            await reddit.RSlashAll.GetPosts(10).ForEachAsync(post =>
            {
                posts.Add(post);
            });

            return posts;
        }
    }
}
