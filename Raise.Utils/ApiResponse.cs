using System;
using System.Net;

namespace Raise.Utils
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            IsSuccess = true;
            ProductionMode = false;
        }

        bool ProductionMode { get; set; }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApiResponse(T data, string message, bool status, HttpStatusCode statusCode)
            : base()
        {
            Data = data;
            Message = message;
            IsSuccess = status;
            StatusCode = statusCode;
        }

        public ApiResponse(T data, Exception exc)
        {
            Data = data;
            Message = ProductionMode ? "Falha interna. Contate o suporte." : $"Message: {exc.Message} / Inner Exception: {exc.InnerException}";
            IsSuccess = false;
            StatusCode = HttpStatusCode.InternalServerError;
        }
    }
}
