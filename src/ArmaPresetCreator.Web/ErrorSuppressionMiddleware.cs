using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ArmaPresetCreator.Web
{
    public class ErrorSuppressionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorSuppressionMiddleware> _logger;

        public ErrorSuppressionMiddleware(
            RequestDelegate next,
            ILogger<ErrorSuppressionMiddleware> logger
            )
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidOperationException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during request");
                throw;
            }
        }
    }
}
