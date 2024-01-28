using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MKExpress.API.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private List<string> _roles;
        public AuthorizeAttribute(string roles)
        {
            _roles = string.IsNullOrEmpty(roles) ? new List<string>() : roles.Split(",").ToList();
        }
        public AuthorizeAttribute()
        {
            _roles = new List<string>();
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var inputRole = context.HttpContext.Items["role"]?.ToString();
            if (string.IsNullOrEmpty(inputRole) || (_roles.Count>0 && !_roles.Any(x => x.ToLower() == inputRole.ToLower())))
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
