using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Raise.Applications;
using Raise.DataBase.FireStorage;
using Raise.DataBase.Postgres;
using Raise.Firebase.Utils;
using Raise.MobileAppService.Interfaces;
using Raise.Model.Models;
using Raise.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Raise.MobileAppService.Repository
{
    public class AccountRepository : FireStorageClient<Account>, IAccountRepository
    {
        static class Accessor
        {
            static AccountBusinessLibrary accountBusinessLibrary;
            internal static AccountBusinessLibrary AccountBusinessLibrary
            {
                get
                {
                    if (accountBusinessLibrary == null)
                        accountBusinessLibrary = new AccountBusinessLibrary();
                    return accountBusinessLibrary;
                }
            }

            static UserRepository userRepository;
            internal static UserRepository UserRepository
            {
                get
                {
                    if (userRepository == null)
                        userRepository = new UserRepository(_serviceScopeFactory);
                    return userRepository;
                }
            }
        }

        public AccountRepository()
        {
            base.GetFirebaseConnection();
        }

        PostgresContext _context;
        static IServiceScopeFactory _serviceScopeFactory;

        public AccountRepository(IServiceScopeFactory serviceScopeFactory)
            : base()
        {
            _serviceScopeFactory = serviceScopeFactory;
            _context = serviceScopeFactory.CreateScope().ServiceProvider.GetService<PostgresContext>();
        }

        public ApiResponse<List<Account>> GetAll()
        {
            try
            {
                var accounts = _context.Account.ToList();
                if (accounts.Count > 0)
                    return new ApiResponse<List<Account>>(accounts, null, true, HttpStatusCode.OK);

                return new ApiResponse<List<Account>>(null, "Contas não encontradas", false, HttpStatusCode.NotFound);
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<List<Account>>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<List<Account>>(null, exc);
            }
        }

        public ApiResponse<Account> GetByObj(Account obj)
        {
            try
            {
                obj.User = Accessor.UserRepository.GetByObj(new User() { GuidKey = obj.GuidKey }).Data;

                var objResponse = _context.Account.Where(p => p.AccountIdenti == obj.AccountIdenti || p.UserIdenti == obj.UserIdenti).FirstOrDefault();
                if (objResponse != null)
                {
                    if (!string.IsNullOrEmpty(objResponse.UrlProfileImage))
                        objResponse.UrlProfileImage = Crypto.Dencryption(obj.UrlProfileImage);
                }
                else 
                {
                    objResponse = new Account();
                }

                // sempre atribui o user para a conta
                objResponse.User = obj.User;

                return new ApiResponse<Account>(objResponse, objResponse.AccountIdenti > 0 ? null : "Conta não encontrada", objResponse.AccountIdenti > 0 ? true : false, objResponse.AccountIdenti > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Account>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Account>(null, exc);
            }
        }

        public ApiResponse<Account> Add(Account obj)
        {
            var apiResponse = new ApiResponse<Account>();

            try
            {
                // realiza upload da imagem de perfil no firestorage
                apiResponse = Upload(obj, obj.GuidKey, new MemoryStream(obj.ProfileImage));
                if (!apiResponse.IsSuccess)
                    return apiResponse;

                // criptogra a url da imagem de perfil
                obj.UrlProfileImage = Crypto.Encryption(apiResponse.Data.ToString());

                // valida informações da conta
                apiResponse = Accessor.AccountBusinessLibrary.InputEntryIsValid(obj);
                if (!apiResponse.IsSuccess)
                    return apiResponse;

                _context.Account.Add(obj);
                apiResponse.IsSuccess = _context.SaveChanges() > 0;

                if (apiResponse.IsSuccess)
                {
                    _context.User.Update(obj.User);
                    apiResponse.IsSuccess = _context.SaveChanges() > 0;
                }

                apiResponse.Message = apiResponse.IsSuccess ? "Contra criada" : "Falha ao criar conta";
                apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                apiResponse.Data = obj;
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Account>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Account>(null, exc);
            }

            return apiResponse;
        }

        public ApiResponse<Account> Update(Account obj)
        {
            var apiResponse = new ApiResponse<Account>();

            try
            {
                _context.Account.Update(obj);
                apiResponse.IsSuccess = _context.SaveChanges() > 0;

                if (apiResponse.IsSuccess)
                {
                    _context.User.Update(obj.User);
                    apiResponse.IsSuccess = _context.SaveChanges() > 0;
                }

                apiResponse.Message = apiResponse.IsSuccess ? "Registro atualizado" : "Falha ao atualizar registro";
                apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                apiResponse.Data = obj;
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Account>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Account>(null, exc);
            }

            return apiResponse;
        }

        public ApiResponse<Account> Delete(long id)
        {
            var apiResponse = GetByObj(new Account() { AccountIdenti = id });

            try
            {
                _context.Account.Remove(apiResponse.Data);
                apiResponse.IsSuccess = _context.SaveChanges() > 0;
                apiResponse.Message = apiResponse.IsSuccess ? "Registro deletado" : "Falha ao deletar registro";
                apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Account>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Account>(null, exc);
            }

            return apiResponse;
        }
    }
}