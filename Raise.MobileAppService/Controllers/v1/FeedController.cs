using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Raise.MobileAppService.Interfaces;
using Raise.Model.Models;
using Raise.Utils;

namespace Raise.MobileAppService.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/feed")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly IFeedRepository _feedRepository;

        public FeedController(IFeedRepository feedRepository)
        {
            _feedRepository = feedRepository;
        }

        [HttpGet]
        public ApiResponse<List<Feed>> GetAll()
        {
            return _feedRepository.GetAll();
        }

        [HttpGet("get")]
        public ApiResponse<Feed> GetByObj([FromBody]Feed feed)
        {
            return _feedRepository.GetByObj(feed);
        }

        [HttpPost]
        public ApiResponse<Feed> Insert([FromBody]Feed feed)
        {
            return _feedRepository.Add(feed);
        }

        [HttpPut]
        public ApiResponse<Feed> Edit([FromBody] Feed feed)
        {
            return _feedRepository.Update(feed);
        }

        [HttpDelete("id")]
        public ApiResponse<Feed> Delete(long id)
        {
            return _feedRepository.Delete(id);
        }
    }
}