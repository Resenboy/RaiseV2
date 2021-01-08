using Raise.Model.Models;
using Raise.Utils;

namespace Raise.Applications
{
    public class AccountBusinessLibrary 
    {
        /// <summary>
        /// Valida informações da conta
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ApiResponse<Account> InputEntryIsValid(Account account)
        {
            var apiResponse = new ApiResponse<Account>();

            if (string.IsNullOrEmpty(account.User.Name))
            {
                apiResponse.Message = "Favor informar seu nome";
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return apiResponse;
            }
            else if (string.IsNullOrEmpty(account.City))
            {
                apiResponse.Message = "Favor informar sua cidade";
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return apiResponse;
            }
            else if (string.IsNullOrEmpty(account.State))
            {
                apiResponse.Message = "Favor informar seu estado";
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return apiResponse;
            }
            else if (string.IsNullOrEmpty(account.GamerTag))
            {
                apiResponse.Message = "Favor informar sua gamer tag";
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return apiResponse;
            }

            return apiResponse;
        }
    }
}
