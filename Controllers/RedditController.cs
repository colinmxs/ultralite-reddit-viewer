using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace UltraliteRedditViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedditController : ControllerBase
    {
        private RedditSharp.RefreshTokenWebAgentPool agentPool;

        public RedditController(RedditSharp.RefreshTokenWebAgentPool webAgentPool)
        {
            agentPool = webAgentPool;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            throw new NotImplementedException();
        }
    }
}
