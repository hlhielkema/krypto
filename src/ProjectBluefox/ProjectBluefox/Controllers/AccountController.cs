using ProjectBluefox.Data.Security;
using ProjectBluefox.Database.Enums;
using ProjectBluefox.Database.Managers;
using ProjectBluefox.Misc;
using ProjectBluefox.Model;
using ProjectBluefox.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZXing;
using ZXing.QrCode;

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
        [ValidateAjaxForm]
        public ActionResult SignIn(SignInModel model)
        {
            try
            {                
                SignInResult result = AccountsManager.Authenticate(model.Username, model.Password);

                if (result.Ok)
                {
                    // Set the authentication cookie
                    FormsAuthentication.SetAuthCookie(result.Username, true);
                }
                else
                {
                    return CreateValidationError(result.Reason);
                }
                
                return JsonOK();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Krypto", "An error occured while trying to sign-in: " + ex.Message);

                return CreateValidationError("Internal Server Error");                
            }
        }

        [HttpGet]
        public ActionResult SignUp(string token)
        {
            // Redirect to the "/"  url when the user is already signed-in
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Account");

            // Validate the token
            if (!string.IsNullOrWhiteSpace(token) && token == InviteTokenCreator.Create())
            {
                return View(new SignUpViewModel()
                {
                    Token = token
                });
            }

            return View("TokenExpired");
        }

        [HttpPost]
        [ValidateAjaxForm]
        public ActionResult SignUp(SignUpModel model)
        {
            // Validate the token
            if (!string.IsNullOrWhiteSpace(model.Token) && model.Token == InviteTokenCreator.Create())
            {
                // Check for "admin" in the username
                if (model.Username.ToLower().Contains("admin"))
                    return CreateValidationError("Username", "Invalid username");

                // Check if the username and email exists
                if (AccountsManager.UsernameExists(model.Username))
                    return CreateValidationError("Username", "The username does already exist.");
                if (AccountsManager.EmailExists(model.Email))
                    return CreateValidationError("Email", "The email address does already exist.");

                // Create the account
                bool ok = AccountsManager.CreateAccount(model.Username.Trim().ToLower(), model.Email.Trim().ToLower(), model.Password);
                if (!ok)
                    return CreateValidationError("Failed to create the user");

                // Set the authentication cookie
                FormsAuthentication.SetAuthCookie(model.Username, true);

                // OK
                return JsonOK();
            }
            else
                // Invalid token, show error on client
                return CreateValidationError("Invalid token");
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
        [RoleRequired(AccountRole.Lurker)]
        public ActionResult Invite()
        {
            IncludeMenu("invite");

            // Create the URL to sign-up
            string url = "https://krypto.frl/signup/" + InviteTokenCreator.Create();
            
            return View(new InviteViewModel(url));
        }

        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.Lurker)]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult InviteQrImage()
        {
            // Create the URL to sign-up
            string url = "https://krypto.frl/signup/" + InviteTokenCreator.Create();

            // Create the writer for the qr code
            BarcodeWriter writer = new BarcodeWriter();
            writer.Options.Height = 300;
            writer.Options.Width = 300;
            writer.Options.Hints.Add(EncodeHintType.MARGIN, 1);
            writer.Format = BarcodeFormat.QR_CODE;

            // Render the QR code image
            Bitmap qrCodeBitmap = writer.Write(url);

            // Convert the QR code bitmap to raw bytes
            ImageConverter converter = new ImageConverter();
            byte[] data = (byte[])converter.ConvertTo(qrCodeBitmap, typeof(byte[]));

            // Return the raw bytes as the request result
            return File(data, "image/bmp");            
        }

        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
            IncludeMenu("changepassword");

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAjaxForm]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (AccountsManager.Authenticate(Username, model.OldPassword).Ok)
            {
                AccountsManager.SetAccountPassword(Username, model.NewPassword);
                return JsonOK();
            }
            else
                return CreateValidationError("OldPassword", "Incorrect password");
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