using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Raise.MobileAppService.Interfaces;
using Raise.Model.Models;
using Raise.Utils;

namespace Raise.MobileAppService.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public ApiResponse<List<Account>> GetAll()
        {
            return _accountRepository.GetAll();
        }

        [HttpGet("get")]
        public ApiResponse<Account> GetByObj([FromBody]Account account)
        {
            return _accountRepository.GetByObj(account);
        }

        [HttpPost]
        public ApiResponse<Account> Insert([FromBody]Account account)
        {
            return _accountRepository.Add(account);
        }

        [HttpPut]
        public ApiResponse<Account> Edit([FromBody] Account account)
        {
            return _accountRepository.Update(account);
        }

        [HttpDelete("id")]
        public ApiResponse<Account> Delete(long id)
        {
            return _accountRepository.Delete(id);
        }
    }
}