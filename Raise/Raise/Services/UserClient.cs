using Raise.Interface;
using Raise.Model.Models;
using Raise.Services.API;
using Raise.Utils;
using RestSharp;

namespace Raise.Services
{
    public class UserClient : ApiClient<User>, IUser
    {
        public ApiResponse<User> Login(User user)
        {
            return BaseRequest(user, "login", Method.POST);
        }
    }
}
