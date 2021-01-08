using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Raise.DataBase.Postgres;
using Raise.Firebase.Utils;
using Raise.MobileAppService.Interfaces;
using Raise.MobileAppService.Repository;
using Raise.Model.Models;
using Raise.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Raise.Repository
{
    public class FeedRepository : IFeedRepository
    {
        static class Accessor
        {
            static PostRepository postRepository;
            internal static PostRepository PostRepository
            {
                get
                {
                    if (postRepository == null)
                        postRepository = new PostRepository(_serviceScopeFactory);
                    return postRepository;
                }
            }
        }

        PostgresContext _context;
        static IServiceScopeFactory _serviceScopeFactory;

        public FeedRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _context = serviceScopeFactory.CreateScope().ServiceProvider.GetService<PostgresContext>();
        }

        public ApiResponse<List<Feed>> GetAll()
        {
            try
            {
                var feed = _context.Feed.ToList();
                if (feed.Count > 0)
                    return new ApiResponse<List<Feed>>(feed, null, true, System.Net.HttpStatusCode.OK);

                return new ApiResponse<List<Feed>>(null, "Posts não encontrados", false, System.Net.HttpStatusCode.NotFound);

            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<List<Feed>>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<List<Feed>>(null, exc);
            }
        }

        public ApiResponse<Feed> GetByObj(Feed obj)
        {
            try
            {
                var followings = _context.Feed.Where(p => p.User.GuidKey == obj.User.GuidKey || p.UserIdenti == obj.UserIdenti).OrderByDescending(p => p.CreateDate);
                if (followings.Count() > 0)
                {
                    foreach (var posts in followings)
                    {
                        var followerPost = new Post();
                        followerPost.UserIdenti = posts.FollowerUserIdenti;
                        var postResponse = Accessor.PostRepository.GetByObj(followerPost);

                        if (!string.IsNullOrEmpty(postResponse.Data.UrlPostImage))
                            postResponse.Data.UrlPostImage = Crypto.Dencryption(postResponse.Data.UrlPostImage);

                        obj.FollowerUserPost.Posts.Add(postResponse.Data);
                    }

                    return new ApiResponse<Feed>(obj, null, true, System.Net.HttpStatusCode.OK);
                }

                return new ApiResponse<Feed>(null, "Não existem posts para serem exibidos", true, System.Net.HttpStatusCode.NotFound);
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Feed>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Feed>(null, exc);
            }
        }

        public ApiResponse<Feed> Add(Feed obj)
        {
            var apiResponse = new ApiResponse<Feed>();

            try
            {
                _context.Feed.Add(obj);
                apiResponse.IsSuccess = _context.SaveChanges() > 0;
                apiResponse.Message = apiResponse.IsSuccess ? "Solicitação enviada" : "Falha ao enviar solicitação";
                apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                apiResponse.Data = obj;
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Feed>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Feed>(null, exc);
            }

            return apiResponse;
        }

        public ApiResponse<Feed> Update(Feed obj)
        {
            var apiResponse = new ApiResponse<Feed>();

            try
            {
                _context.Feed.Update(obj);
                apiResponse.IsSuccess = _context.SaveChanges() > 0;
                apiResponse.Message = apiResponse.IsSuccess ? "Registro atualizado" : "Falha ao atualizar registro";
                apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                apiResponse.Data = obj;
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Feed>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Feed>(null, exc);
            }

            return apiResponse;
        }

        public ApiResponse<Feed> Delete(long id)
        {
            var apiResponse = GetByObj(new Feed() { FeedIdenti = id });

            try
            {
                _context.Feed.Remove(apiResponse.Data);
                apiResponse.IsSuccess = _context.SaveChanges() > 0;
                apiResponse.Message = apiResponse.IsSuccess ? "Registro deletado" : "Falha ao deletar registro";
                apiResponse.StatusCode = apiResponse.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            }
            catch (NpgsqlException exc)
            {
                return new ApiResponse<Feed>(null, exc);
            }
            catch (Exception exc)
            {
                return new ApiResponse<Feed>(null, exc);
            }

            return apiResponse;
        }
    }
}
