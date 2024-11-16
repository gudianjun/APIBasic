using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace APIBasic.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // 自定义授权逻辑
            if (context.HttpContext.User.Identity != null && 
                !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
