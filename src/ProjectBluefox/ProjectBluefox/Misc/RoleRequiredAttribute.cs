using ProjectBluefox.Controllers;
using ProjectBluefox.Database.Enums;
using ProjectBluefox.Database.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBluefox.Misc
{
    public class RoleRequiredAttribute :  FilterAttribute, IAuthorizationFilter
    {
        private AccountRole _role;

        public RoleRequiredAttribute(AccountRole role)
        {
            _role = role;
        }

        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            BaseController ctrl = filterContext.Controller as BaseController;
            if (ctrl == null || !ctrl.IsAuthenticated || !ctrl.HasRole(_role))
                filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}