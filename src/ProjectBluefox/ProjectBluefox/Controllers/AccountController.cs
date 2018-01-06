using ProjectBluefox.Database.Enums;
using ProjectBluefox.Database.Managers;
using ProjectBluefox.Misc;
using ProjectBluefox.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectBluefox.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                // Redirect to the default start page
                return RedirectToAction("Index", "Currencies");
            }
            else
            {
                // Redirect to sign-in
                return RedirectToAction("SignIn", "Account");
            }
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            // Redirect to the "/"  url when the user is already signed-in
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Account");

            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string username, string password)
        {            
            try
            {
                SignInResult result = AccountsManager.Authenticate(username, password);
                
                if (result.Ok)
                {
                    // Set the authentication cookie
                    FormsAuthentication.SetAuthCookie(result.Username, true);
                }

                return Json(result);
            }
            catch (Exception ex)
            {                
                EventLog.WriteEntry("Krypto", "An error occured while trying to sign-in: " + ex.Message);

                return Json(new SignInResult(false, username)
                {
                    Reason = "Internal Server Error"
                });
            }            
        }

        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.SystemAdministrator)]
        public ActionResult Accounts()
        {
            IncludeMenu("accounts");

            return View();
        }

        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.SystemAdministrator)]
        public ActionResult GetAccounts()
        {
            try
            {
                return Json(AccountsManager.GetAccounts(), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.SystemAdministrator)]
        public ActionResult CreateAccount()
        {
            IncludeMenu("accounts");

            return View();
        }

        [HttpPost]
        [Authorize]
        [RoleRequired(AccountRole.SystemAdministrator)]
        public ActionResult CreateAccount(CreateAccountModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Json(AccountsManager.CreateAccount(model.Username, model.EmailAddress, model.Password), JsonRequestBehavior.AllowGet);
                }
                else
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult SignOut()
        {
            // Sign-out   
            FormsAuthentication.SignOut();
            
            // Redirect to the sign-in page
            return RedirectToAction("SignIn", "Account");
        }
    }
}