using ProjectBluefox.Database.Enums;
using ProjectBluefox.Database.Managers;
using ProjectBluefox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectBluefox.Controllers
{
    public class BaseController : Controller
    {
        public string Username
        {
            get
            {
                if (Request.IsAuthenticated)
                    return System.Web.HttpContext.Current.User.Identity.Name.ToLower();
                else
                    return null;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return Request.IsAuthenticated;
            }
        }

        public bool HasRole(AccountRole role)
        {
            return Request.IsAuthenticated && AccountsManager.HasRole(Username, role);
        }

        public void IncludeMenu(string active)
        {
            ViewBag.MenuButtons = GetMenuItems(active).ToList();
        }

        public IEnumerable<NavigationMenuButton> GetMenuItems(string active)
        {
            yield return new NavigationMenuButton()
            {
                DisplayName = "Currencies",
                Icon = "fab fa-ethereum",
                Url = "/Currencies/Index",
                IsActive = active == "currencies",
            };

            yield return new NavigationMenuButton()
            {
                DisplayName = "Links",
                Icon = "fas fa-link",
                Url = "/Links/Index",
                IsActive = active == "links",
            };

            yield return new NavigationMenuButton()
            {
                DisplayName = "Invite",
                Icon = "far fa-plus-square",
                Url = "/Account/Invite",
                IsActive = active == "invite",
            };

            yield return new NavigationMenuButton()
            {
                DisplayName = "Password",
                Icon = "fas fa-key",
                Url = "/Account/ChangePassword",
                IsActive = active == "changepassword",
            };

            if (HasRole(AccountRole.SystemAdministrator))
            {
                yield return new NavigationMenuButton()
                {
                    DisplayName = "Accounts",
                    Icon = "fas fa-users",
                    Url = "/Account/Accounts",
                    IsActive = active == "accounts",
                };
            }
        }

        protected JsonResult JsonOK()
        {
            return Json(new { OK = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FormatValidationErrors()
        {
            Dictionary<string, string> messages = new Dictionary<string, string>();
            foreach (KeyValuePair<string, ModelState> field in ModelState)
            {
                if (field.Value.Errors.Any())
                {
                    messages.Add(field.Key, field.Value.Errors.First().ErrorMessage);
                }
            }
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 412; // invalid request
            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult CreateValidationError(string message)
            => CreateValidationError("*", message);

        protected ActionResult CreateValidationError(string name, string message)
        {
            Dictionary<string, string> messages = new Dictionary<string, string>
            {
                { name, message }
            };
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 412; // invalid request
            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult CreateValidationErrors(params KeyValuePair<string, string>[] errors)
        {
            Dictionary<string, string> messages = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> field in errors)
                messages.Add(field.Key, field.Value);
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 412; // invalid request
            return Json(messages, JsonRequestBehavior.AllowGet);
        }
    }
}