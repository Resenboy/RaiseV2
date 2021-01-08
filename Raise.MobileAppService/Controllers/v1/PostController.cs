using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Raise.MobileAppService.Interfaces;
using Raise.Model.Models;
using Raise.Utils;

namespace Raise.MobileAppService.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public ApiResponse<List<Post>> GetAll()
        {
            return _postRepository.GetAll();
        }

        [HttpGet("get")]
        public ApiResponse<Post> GetByObj([FromBody] Post Post)
        {
            return _postRepository.GetByObj(Post);
        }

        [HttpPost]
        public ApiResponse<Post> Insert([FromBody] Post Post)
        {
            return _postRepository.Add(Post);
        }

        [HttpPut]
        public ApiResponse<Post> Edit([FromBody] Post Post)
        {
            return _postRepository.Update(Post);
        }

        [HttpDelete("id")]
        public ApiResponse<Post> Delete(long id)
        {
            return _postRepository.Delete(id);
        }
    }
}
