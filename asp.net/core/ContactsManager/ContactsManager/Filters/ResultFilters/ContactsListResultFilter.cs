using ContactsManager.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.Filters.ResultFilters
{
    // result filters: last moment changes to the response
    public class ContactsListResultFilter : IAsyncResultFilter
    {
        private readonly ILogger<ContactsListResultFilter> _logger;

        public ContactsListResultFilter(ILogger<ContactsListResultFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            _logger.LogInformation("-----> {FilterName} ---> {MethodName} (before)",
               nameof(ContactsListResultFilter),
               nameof(OnResultExecutionAsync)
            );
            context.HttpContext.Response.Headers.LastModified = DateTime.Now.ToString();

            await next(); // call the subsequent filter of IActionResult

            _logger.LogInformation("-----> {FilterName} ---> {MethodName} (after)",
               nameof(ContactsListResultFilter),
               nameof(OnResultExecutionAsync)
            );
            // InvalidOperationException
            //context.HttpContext.Response.Headers["Last-Modified"] = DateTime.Now.ToString();
        }
    }
}
