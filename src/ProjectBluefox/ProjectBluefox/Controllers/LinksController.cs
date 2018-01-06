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
    public class LinksController : BaseController
    {
        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.Lurker)]
        public ActionResult Index()
        {
            IncludeMenu("links");
            return View();
        }

        [HttpGet]
        [Authorize]
        [RoleRequired(AccountRole.Lurker)]
        public ActionResult GetLinks()
        {
            try
            {
                return Json(LinksManager.GetAllLinks(), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}