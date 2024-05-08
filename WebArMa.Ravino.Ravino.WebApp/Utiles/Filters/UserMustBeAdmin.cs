using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebArMa.Ravino.Ravino.WebApp.Utiles.Filters
{
    public class UserMustBeAdmin : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIsAdmin = context.HttpContext.User.IsInRole("Admin");

            if (!userIsAdmin)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
