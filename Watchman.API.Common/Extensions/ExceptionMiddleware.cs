using Microsoft.AspNetCore.Http;

using System;
using System.Net;
using System.Threading.Tasks;

namespace Watchman.API.Common.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int code;
            switch (ex)
            {
                case ArgumentException _:
                    {
                        code = (int)HttpStatusCode.BadRequest;
                        break;
                    }
                default:
                    {
                        code = (int)HttpStatusCode.InternalServerError;
                        break;
                    }
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = code,
                Message = ex.Message
            }.ToString());
        }
    }
}
