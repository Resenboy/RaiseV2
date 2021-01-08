using Raise.Model.Models;
using Raise.Utils;
using System;
using System.Text.RegularExpressions;

namespace Raise.Applications
{
    public class UserBusinessLibrary
    {
       public bool IsValidEmail(string email)
        {
            var IsValid = false;
            if (email != null)
            {
                const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
                IsValid = Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            return IsValid;
        }


        /// <summary>
        /// Valida se o usuario informou email e senha
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiResponse<User> InputEntryIsValid(User user)
        {
            var apiResponse = new ApiResponse<User>();

            if (!IsValidEmail(user.Email))
            {
                apiResponse.Message = "Favor informar um email válido";
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return apiResponse;
            }
            else if (string.IsNullOrEmpty(user.Password))
            {
                apiResponse.Message = "Favor informar a senha";
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return apiResponse;
            }

            return apiResponse;
        }

        public ApiResponse<User> ValidCredentials(User userRequest, User userReponse)
        {
            var apiResponse = new ApiResponse<User>();

            if (userReponse == null)
            {
                apiResponse.Message = "Usuário e/ou senha inválidos";
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            else if (PasswordGenerate.Dencryption(userRequest.Password) != PasswordGenerate.Dencryption(userReponse.Password))
            {
                apiResponse.Message = "Usuário e/ou senha inválidos";
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            return apiResponse;
        }
    }
}
