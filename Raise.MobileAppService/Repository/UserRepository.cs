using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Raise.Applications;
using Raise.DataBase.Postgres;
using Raise.MobileAppService.Interfaces;
using Raise.Model.Models;
using Raise.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Raise.MobileAppService.Repository
{
    public class UserRepository : IUserRepository
    {
        static class Accessor
        {
            static UserBusinessLibrary userBusinessLibrary;
            internal static UserBusinessLibrary UserBusinessLibrary
            {
                get
                {
                    if (userBusinessLibrary == null)
                        userBusinessLibrary = new UserBusinessLibrary();
                    return userBusinessLibrary;
                }
            }
        }

        PostgresContext _context;

        public UserRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _context = serviceScopeFactory.CreateScope().ServiceProvider.GetService<PostgresContext>();
        }

        public ApiResponse<List<User>> GetAll()
        {
            try
            {
                var users = _context.User.ToList();
                if (users.Count > 0)
                    return new ApiResponse<List<User>>(users, null, true, HttpStatusCode.OK);

                return new ApiResponse<List<User>>(null, "Usuários não encontrados", false, HttpStatusCode.NotFound);
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<List<User>>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<List<User>>(null, exc);
            }
        }

        public ApiResponse<User> GetByObj(User user)
        {
            try
            {
                var objResponse = _context.User.Where(p => p.GuidKey == user.GuidKey || p.UserIdenti == user.UserIdenti).FirstOrDefault();
                if (objResponse != null)
                    return new ApiResponse<User>(objResponse, null, true, HttpStatusCode.OK);

                return new ApiResponse<User>(null, "Usuário e/ou senha inválidos", false, HttpStatusCode.NotFound);
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<User>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<User>(null, exc);
            }
        }

        public ApiResponse<User> Add(User obj)
        {
            var apiResponse = GetByObj(obj);

            try
            {
                if (apiResponse.IsSuccess)
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = "Usuário já cadastrado";
                    return apiResponse;
                }

                if (!apiResponse.IsSuccess && apiResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    apiResponse.Message = null;

                    _context.User.Add(obj);
                    apiResponse.IsSuccess = _context.SaveChanges() > 0;
                    apiResponse.Message = apiResponse.IsSuccess ? "Usuário adicionado" : "Falha ao criar usuário";
                    apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                    apiResponse.Data = obj;
                }
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<User>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<User>(null, exc);
            }

            return apiResponse;
        }

        public ApiResponse<User> Update(User obj)
        {
            var apiResponse = new ApiResponse<User>();

            try
            {
                _context.User.Update(obj);
                apiResponse.IsSuccess = _context.SaveChanges() > 0;
                apiResponse.Message = apiResponse.IsSuccess ? "Registro atualizado" : "Falha ao atualizar registro";
                apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                apiResponse.Data = obj;
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<User>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<User>(null, exc);
            }

            return apiResponse;
        }

        public ApiResponse<User> Delete(long id)
        {
            var apiResponse = GetByObj(new User() { UserIdenti = id });

            try
            {
                _context.User.Remove(apiResponse.Data);
                apiResponse.IsSuccess = _context.SaveChanges() > 0;
                apiResponse.Message = apiResponse.IsSuccess ? "Registro deletado" : "Falha ao deletar registro";
                apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<User>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<User>(null, exc);
            }

            return apiResponse;
        }

        public ApiResponse<User> Login(User user)
        {
            // valida se o usuario informou email e senha
            var apiResponse = Accessor.UserBusinessLibrary.InputEntryIsValid(user);

            try
            {
                if (!apiResponse.IsSuccess)
                    return apiResponse;

                var objResponse = _context.User.Where(p => p.GuidKey.ToString() == user.GuidKey.ToString()).FirstOrDefault();

                // Valida se as credencias estão válidas
                apiResponse = Accessor.UserBusinessLibrary.ValidCredentials(user, objResponse);
                if (!apiResponse.IsSuccess)
                    return apiResponse;

                return new ApiResponse<User>(objResponse, null, apiResponse.IsSuccess, HttpStatusCode.OK);
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<User>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<User>(null, exc);
            }
        }
    }
}
