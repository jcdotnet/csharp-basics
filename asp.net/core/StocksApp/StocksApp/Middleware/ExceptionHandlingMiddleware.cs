using Exceptions;

namespace StocksApp.Middleware
{
    // requirements
    // ExceptionHandlingMiddleware:
    // Create a custom middleware called 'ExceptionHandlingMiddleware'
    // and apply it in earliest position in the middleware pipiline.

    // It handles any exception that occur in all subsequent middleware, action method and filters.

    // When an exception is caught, it logs that same exception details
    // and rethrow it - so that,the same exception can be caught by UseExceptionHandler() middleware
    // added earlier in the middleware pipeline.

    // The built-in UseExceptionHandler() middleware shows user-friendly error message
    // using "/Home/Error" view.
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<Exception> _logger;

        public ExceptionHandlingMiddleware(ILogger<Exception> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context); // subsequent middleware, action methods, etc.
            }
            catch (FinnhubException ex)
            {
                LogException(ex);

                //context.Response.StatusCode = 500;
                //await context.Response.WriteAsync(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                LogException(ex);

                //context.Response.StatusCode = 500;
                //await context.Response.WriteAsync(ex.Message);
                throw;
            }
        }

        private void LogException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                if (ex.InnerException.InnerException != null)
                {
                    _logger.LogError("{ExceptionType} {ExceptionMessage}",
                        ex.InnerException.InnerException.GetType().ToString(),
                        ex.InnerException.InnerException.Message);
                }
                else
                {
                    _logger.LogError("{ExceptionType} {ExceptionMessage}",
                        ex.InnerException.GetType().ToString(),
                        ex.InnerException.Message);
                }
            }
            else
            {
                _logger.LogError("{ExceptionType} {ExceptionMessage}",
                    ex.GetType().ToString(),
                    ex.Message);

            }
        }
    }
}