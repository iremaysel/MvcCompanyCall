using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCompanyCall.Models.Entity;

namespace MvcCompanyCall.Controllers
{
    [Authorize]
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
        
        [HttpGet]
        public ActionResult NewCall()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCall(TblCall p)
        {
            p.CallStatus = true;
            p.CallDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.CallCompany = 4;
            db.TblCall.Add(p);
            db.SaveChanges();
            return RedirectToAction("ActiveCalls");
        }

        public ActionResult CallDetail(int id)
        {
            var call = db.TblCallDetail.Where(x => x.DetailCall == id).ToList();
            return View(call);
        }

        public ActionResult BringToCall(int id)
        {
            var call = db.TblCall.Find(id);
            return View("BringToCall", call);
        }

        public ActionResult CallUpdate(TblCall p)
        {
            var calls = db.TblCall.Find(p.ID);
            calls.CallSubject = p.CallSubject;
            calls.CallStatement = p.CallStatement;
            db.SaveChanges();
            return RedirectToAction("ActiveCalls");
        }
    }

}