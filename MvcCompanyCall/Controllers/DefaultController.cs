using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCompanyCall.Models.Entity;

namespace MvcCompanyCall.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        DbJobTrackingEntities db = new DbJobTrackingEntities();
        public ActionResult ActiveCalls()
        {
            var calls = db.TblCall.Where(x =>x.CallStatus == true && x.CallCompany == 4).ToList();
            return View(calls);
        }
        public ActionResult PassiveCalls()
        {
            var calls = db.TblCall.Where(x => x.CallStatus == false && x.CallCompany == 4).ToList();
            return View(calls);
        }
    }
}