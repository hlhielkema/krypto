using ProjectBluefox.Database.Enums;
using ProjectBluefox.Database.Managers;
using ProjectBluefox.Misc;
using ProjectBluefox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectBluefox.Controllers
{
    public class CurrenciesController : BaseController
    {
        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.Lurker)]
        public ActionResult Index()
        {
            IncludeMenu("currencies");
            return View();
        }

        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.Lurker)]
        public ActionResult GetCurrencies()
        {            
            try
            {
                return Json(CurrenciesManager.GetCurrencies(), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.Editor)]
        public ActionResult CreateCurrency()
        {
            IncludeMenu("currencies");
            return View();
        }

        [HttpPost]
        [Authorize]
        [RoleRequired(AccountRole.Editor)]
        public ActionResult CreateCurrency(string displayName, string shortCode)
        {
            try
            {
                Guid currencyId = CurrenciesManager.CreateCurrency(displayName, shortCode, Username);
                return Json(currencyId);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.Lurker)]
        public ActionResult Currency(string id) // id = shortCode
        {
            IncludeMenu("currencies");
            if (CurrenciesManager.CurrencyExists(id))
                return View(CurrenciesManager.GetCurrencyByShortCode(id).Id);
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.Lurker)]
        public ActionResult GetCurrency(string id) // id = currency id
        {
            try
            {
                if (Guid.TryParse(id, out Guid currencyId) && CurrenciesManager.CurrencyExists(currencyId))
                    return Json(CurrenciesManager.GetCurrencyById(currencyId), JsonRequestBehavior.AllowGet);
                else
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.Lurker)]
        public ActionResult GetComments(string id)
        {
            try
            {
                if (Guid.TryParse(id, out Guid currencyId) && CurrenciesManager.CurrencyExists(currencyId))
                    return Json(CurrenciesManager.GetComments(currencyId), JsonRequestBehavior.AllowGet);
                else
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Authorize]
        [RoleRequired(AccountRole.Editor)]
        public ActionResult CreateComment(CreateCommentModel model)
        {            
            try
            {
                if (ModelState.IsValid)
                {
                    if (CurrenciesManager.CurrencyExists(model.Currency))
                        return Json(CurrenciesManager.CreateComment(model.Currency, model.Message, model.Vote, Username), JsonRequestBehavior.AllowGet);
                    else
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                else
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);                
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}