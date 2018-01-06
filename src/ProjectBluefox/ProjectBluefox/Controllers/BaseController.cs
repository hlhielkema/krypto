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
                Url = "/Currencies/Index",
                IsActive = active == "currencies",
            };

            yield return new NavigationMenuButton()
            {
                DisplayName = "Links",
                Url = "/Links/Index",
                IsActive = active == "links",
            };

            if (HasRole(AccountRole.SystemAdministrator))
            {
                yield return new NavigationMenuButton()
                {
                    DisplayName = "Accounts",
                    Url = "/Account/Accounts",
                    IsActive = active == "accounts",
                };
            }
        }
    }
}