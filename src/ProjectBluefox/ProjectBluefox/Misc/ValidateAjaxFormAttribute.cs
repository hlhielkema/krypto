using ProjectBluefox.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBluefox.Misc
{
    public class ValidateAjaxFormAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller is BaseController controller)
            {
                if (!controller.ModelState.IsValid)
                {
                    filterContext.Result = controller.FormatValidationErrors();
                }
            }
            else
                throw new InvalidOperationException();            
        }
    }
}