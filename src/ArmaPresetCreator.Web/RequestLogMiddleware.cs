using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ArmaPresetCreator.Web
{
    public class RequestLogMiddleware : IMiddleware
    {
        private readonly ILogger<RequestLogMiddleware> requestLogMiddlewareLogger;

        public RequestLogMiddleware(ILogger<RequestLogMiddleware> requestLogMiddlewareLogger)
        {
            this.requestLogMiddlewareLogger = requestLogMiddlewareLogger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Uri GetAbsoluteUri(HttpRequest httpRequest)
            {
                var uriBuilder = new UriBuilder(httpRequest.Scheme, httpRequest.Host.Host, httpRequest.Host.Port.GetValueOrDefault(), httpRequest.Path.Value ?? string.Empty);
                return uriBuilder.Uri;
            }

            void LogRequestMessage(HttpContext httpContext, bool isError)
            {
                string logMessage;
                Action<string> logAction;

                var requestUri = GetAbsoluteUri(httpContext.Request);

                if (!isError)
                {
                    logMessage = $"Incoming Request: Request URL: {requestUri} Method: {context.Request.Method}";
                    logAction = (message) => requestLogMiddlewareLogger.LogInformation(message);
                } else
                {
                    logMessage = $"Error occurred: Request URL: {requestUri} Method: {context.Request.Method}";
                    logAction = (message) => requestLogMiddlewareLogger.LogError(logMessage);
                }

                logAction(logMessage);
            }

            try
            {
                LogRequestMessage(context, false);

                await next(context);
            }
            catch (Exception ex)
            {
                LogRequestMessage(context, true);
                throw;
            }
        }
    }
}
