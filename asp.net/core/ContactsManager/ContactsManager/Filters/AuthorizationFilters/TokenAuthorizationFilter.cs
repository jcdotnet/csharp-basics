using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.Filters.AuthorizationFilters
{
    public class TokenAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Cookies.ContainsKey("Auth-Key") == false ||
                context.HttpContext.Request.Cookies["Auth-Key"] != "ExpectedValue")
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                //return;
            }
        }
    }
}
