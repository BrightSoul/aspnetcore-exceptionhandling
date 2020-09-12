using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ExceptionDemo.Models.Exceptions;

namespace ExceptionDemo.Models.Middlewares
{
    public class MyMiddleware
    {
        private readonly RequestDelegate next;

        public MyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Call the next delegate/middleware in the pipeline
            if (ShouldAttemptDatabaseConnection(context))
            {
                try
                {
                    // Connessione al database
                    throw new Exception("Error");
                }
                catch (Exception exc)
                {
                    // La connessione al database è fallita per qualche motivo
                    // Allora risollevo un'eccezione più significativa
                    throw new MyDatabaseException("Problem communicating witht the database", exc);
                }
            }
            // Invoco il middleware successivo
            await next(context);
        }

        private bool ShouldAttemptDatabaseConnection(HttpContext context)
        {
            var feature = context.Features.Get<IExceptionHandlerPathFeature>();
            return feature == null;
        }
    }
}