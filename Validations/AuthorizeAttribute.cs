using Microsoft.AspNetCore.Mvc.Filters;
using MKExpress.API.Constants;
using MKExpress.API.Enums;
using MKExpress.API.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MKExpress.API.Validations
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter, IFilterMetadata
    {
        private readonly List<UserRoleEnum> _role;

        public AuthorizeAttribute(params UserRoleEnum[] role)
        {
            _role = role.ToList();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string header = context.HttpContext.Request.Headers["x-user-type"];
            if (string.IsNullOrEmpty(header))
                throw new UnauthorizedException("No x-user-type header found!");
            if (!StaticValues.UserTypeRoleMap.ContainsKey(header))
                throw new BusinessRuleViolationException("UnsupportedUserType", "This user type is not supported!");
            if (!_role.Contains(StaticValues.UserTypeRoleMap[header]))
                throw new ForbiddenException();
        }
    }
}