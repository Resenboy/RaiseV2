using Newtonsoft.Json;
using Raise.Interface.Base;
using Raise.Model.Models;
using Raise.Utils;
using RestSharp;
using System;

namespace Raise.Services.API
{
    public class ApiClient<T> : RestClient, IApiClient<T> where T : BaseModel
    {
        /// <summary>
        /// Azure
        /// </summary>
        private const string _URL = "https://raisemobileappservice.azurewebsites.net/api/v1/";

        /// <summary>
        /// Local
        /// </summary>
        //private const string _URL = "http://localhost:54013/api/v1/";

        /// <summary>
        /// IIS
        /// </summary>
        //private const string _URL = "http://192.168.7.107:54013/api/v1/"; 

        public ApiClient()
        {
            base.BaseUrl = new Uri($"{_URL}");
        }

        //TODO: utilizar para realizar ações na BaseModel local do aparelho
        public bool IsConnected => true; //Connectivity.NetworkAccess != NetworkAccess.Internet;

        public ApiResponse<T> GetAll()
        {
            return BaseRequest(null, string.Empty, Method.GET);
        }

        public ApiResponse<T> GetByObj(T obj)
        {
            return BaseRequest(obj, "get", Method.GET);
        }

        public ApiResponse<T> Add(T obj)
        {
            return BaseRequest(obj, null, Method.POST);
        }

        public ApiResponse<T> Update(T obj)
        {
            return BaseRequest(obj, null, Method.PUT);  
        }

        public ApiResponse<T> Delete(long id)
        {
            return BaseRequest(id, Method.DELETE);
        }

        protected ApiResponse<T> NullObject()
        {
            return new ApiResponse<T>()
            {
                IsSuccess = false,
                Data = null,
                Message = "Objeto null.",
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }

        ApiResponse<T> BaseRequest(long id, Method method)
        {
            return BaseRequest(null, string.Empty, method, id);
        }

        protected ApiResponse<T> BaseRequest(T obj, string route, Method method)
        {
            return BaseRequest(obj, route, method, 0);
        }

        /// <summary>
        /// Método que reliza a chamada para API seperada por metodos
        /// </summary>
        /// <param name="obj"> Model do objeto </param>
        /// <param name="route"> Rota do controller para metodos POST, caso exista </param>
        /// <param name="method"> Tipo do método> GET, POST, PUT e DELETE </param>
        /// <param name="id"> Id do registro a ser apagado - utilizado apenas no DELETE</param>
        /// <returns></returns>
        ApiResponse<T> BaseRequest(T obj, string route, Method method, long id)
        {
            RestRequest request = null;
            ApiResponse<T> apiResponse = null;
            IRestResponse response = null;

            switch (method)
            {
                case Method.GET:
                    if (route == null)
                    {
                        request = new RestRequest($"{typeof(T).Name.ToLower()}", method);
                        request.RequestFormat = DataFormat.Json;
                        response = base.Execute(request);
                        apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
                    }
                    else
                    {
                        request = new RestRequest($"{typeof(T).Name.ToLower()}/{route}", method);
                        request.RequestFormat = DataFormat.Json;
                        request.AddJsonBody(obj);
                        response = base.Execute(request);
                        apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
                    }
                    break;

                case Method.POST:
                    if (obj == null || !IsConnected)
                        return NullObject();

                    if (route == null)
                    {
                        obj.CreateDate = DateTime.Now;

                        request = new RestRequest($"{typeof(T).Name.ToLower()}", method);
                        request.RequestFormat = DataFormat.Json;
                        request.AddJsonBody(obj);
                        response = base.Execute(request);
                        apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
                    }
                    else
                    {
                        request = new RestRequest($"{typeof(T).Name.ToLower()}/{route}", method);
                        request.RequestFormat = DataFormat.Json;
                        request.AddJsonBody(obj);
                        response = base.Execute(request);
                        apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
                    }
                    break;

                case Method.PUT:
                    if (obj == null || !IsConnected)
                        return NullObject();

                    obj.UpdateDate = DateTime.Now;

                    request = new RestRequest($"{typeof(T).Name.ToLower()}", method);
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(obj);
                    response = base.Execute(request);
                    apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
                    break;

                case Method.DELETE:
                    if (id == 0 && !IsConnected)
                        return NullObject();

                    request = new RestRequest($"{typeof(T).Name.ToLower()}/{route}", Method.DELETE);
                    request.AddParameter(route, id, ParameterType.UrlSegment);
                    request.RequestFormat = DataFormat.Json;
                    response = base.Execute(request);
                    apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
                    break;
            }

            if(apiResponse == null)
                return NullObject();

            return apiResponse;
        }
    }
}
