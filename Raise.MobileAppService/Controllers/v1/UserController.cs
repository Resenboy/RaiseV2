using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Raise.MobileAppService.Interfaces;
using Raise.Model.Models;
using Raise.Utils;

namespace Raise.MobileAppService.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //MapToApiVersion( "2" )
        [HttpGet]
        public ApiResponse<List<User>> GetAll()
        {
            return _userRepository.GetAll();
        }

        [HttpGet("get")]
        public ApiResponse<User> GetByObj([FromBody] User user)
        {
            return _userRepository.GetByObj(user);
        }

        [HttpPost]
        public ApiResponse<User> Insert([FromBody] User user)
        {
            return _userRepository.Add(user);
        }

        [HttpPut]
        public ApiResponse<User> Edit([FromBody] User user)
        {
            return _userRepository.Update(user);
        }

        [HttpDelete("id")]
        public ApiResponse<User> Delete(long id)
        {
            return _userRepository.Delete(id);
        }

        [HttpPost("login")]
        public ApiResponse<User> Login([FromBody] User user)
        {
            return _userRepository.Login(user);
        }
    }
}