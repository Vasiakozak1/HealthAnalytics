using HealthAnalytics.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HealthAnalytics.Web.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate next;

        public ExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var managedException = exception as ApiException;
            context.Response.ContentType = "application/json";
            if (managedException != null)
            {
                context.Response.StatusCode = (int)managedException.StatusCode;
                return context.Response.WriteAsync(exception.Message);
            }
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(exception.Message);
        }
    }
}
