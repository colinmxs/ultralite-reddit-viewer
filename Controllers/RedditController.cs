using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace UltraliteRedditViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedditController : ControllerBase
    {
        [HttpGet]
        public async Task<object> Get()
        {
            throw new NotImplementedException();
        }
    }
}
