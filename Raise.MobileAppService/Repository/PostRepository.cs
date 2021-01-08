using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Raise.DataBase.FireStorage;
using Raise.DataBase.Postgres;
using Raise.Firebase.Utils;
using Raise.MobileAppService.Interfaces;
using Raise.Model.Models;
using Raise.Utils;

namespace Raise.MobileAppService.Repository
{
    public class PostRepository : FireStorageClient<Post>, IPostRepository
    {
        public PostRepository()
        {
            base.GetFirebaseConnection();
        }

        PostgresContext _context;

        public PostRepository(IServiceScopeFactory serviceScopeFactory)
            : base()
        {
            _context = serviceScopeFactory.CreateScope().ServiceProvider.GetService<PostgresContext>();
        }


        public ApiResponse<List<Post>> GetAll()
        {
            try
            {
                var posts = _context.Post.ToList();
                if (posts.Count > 0)
                    return new ApiResponse<List<Post>>(posts, null, true, System.Net.HttpStatusCode.OK);

                return new ApiResponse<List<Post>>(null, "Posts não encontrados", false, System.Net.HttpStatusCode.NotFound);
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<List<Post>>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<List<Post>>(null, exc);
            }
        }

        public ApiResponse<Post> GetByObj(Post obj)
        {
            try
            {
                var post = _context.Post.Where(p => p.GuidKey == obj.GuidKey || p.UserIdenti == obj.UserIdenti).LastOrDefault();
                if (post != null)
                    return new ApiResponse<Post>(post, null, true, System.Net.HttpStatusCode.OK);

                return new ApiResponse<Post>(null, "Post não encontrado", false, System.Net.HttpStatusCode.NotFound);
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Post>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Post>(null, exc);
            }
        }

        public ApiResponse<Post> Add(Post obj)
        {
            try
            {
                var apiResponse = UploadPost(obj, obj.PostGuid, new MemoryStream(obj.PostImage));
                if (!apiResponse.IsSuccess)
                    return apiResponse;

                obj.UrlPostImage = Crypto.Encryption(apiResponse.Data.ToString());

                _context.Post.Add(obj);
                apiResponse.IsSuccess = _context.SaveChanges() > 0;
                apiResponse.Message = apiResponse.IsSuccess ? "Post realizado" : "Falha ao enviar postar";
                apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                apiResponse.Data = obj;

                return apiResponse;
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Post>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Post>(null, exc);
            }
        }

        public ApiResponse<Post> Update(Post obj)
        {
            var apiResponse = new ApiResponse<Post>();

            try
            {
                _context.Post.Update(obj);
                apiResponse.IsSuccess = _context.SaveChanges() > 0;
                apiResponse.Message = apiResponse.IsSuccess ? "Registro atualizado" : "Falha ao atualizar registro";
                apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                apiResponse.Data = obj;
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Post>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Post>(null, exc);
            }

            return apiResponse;
        }

        public ApiResponse<Post> Delete(long id)
        {
            var apiResponse = GetByObj(new Post() { PostIdenti = id });

            try
            {
                _context.Post.Remove(apiResponse.Data);
                apiResponse.IsSuccess = _context.SaveChanges() > 0;
                apiResponse.Message = apiResponse.IsSuccess ? "Registro deletado" : "Falha ao deletar registro";
                apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Post>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Post>(null, exc);
            }

            return apiResponse;
        }
    }
}
