using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Threading.Tasks;
using UPB.BusinessLogic.Manager.Exceptions;

namespace UPB.PracticeDos.Midlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private string _currentEnvironment;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            _currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (_currentEnvironment.Equals("Development"))
            {
                Log.Error(exception.Message);
            }
            else if (_currentEnvironment.Equals("QA"))
            {
                Log.Information(exception.Message);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            }.ToString());
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHnadlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHnadlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
