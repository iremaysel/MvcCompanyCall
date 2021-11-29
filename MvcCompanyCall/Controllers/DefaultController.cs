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
            var email = (string)Session["Email"];
            var id = db.TblCompanies.Where(x => x.Email == email).Select(y => y.ID).FirstOrDefault();
            var calls = db.TblCall.Where(x => x.CallStatus == true && x.CallCompany == id).ToList();
            return View(calls);
        }


        public ActionResult PassiveCalls()
        {
            var email = (string)Session["Email"];
            var id = db.TblCompanies.Where(x => x.Email == email).Select(y => y.ID).FirstOrDefault();
            var calls = db.TblCall.Where(x => x.CallStatus == false && x.CallCompany == id).ToList();
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
            var email = (string)Session["Email"];
            var id = db.TblCompanies.Where(x => x.Email == email).Select(y => y.ID).FirstOrDefault();
            p.CallStatus = true;
            p.CallDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.CallCompany = id;
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

        [HttpGet]
        public ActionResult ProfileEdit()
        {
            var email = (string)Session["Email"];
            var id = db.TblCompanies.Where(x => x.Email == email).Select(y => y.ID).FirstOrDefault();
            var profile = db.TblCompanies.Where(x => x.ID == id).FirstOrDefault();
            return View(profile);
        }

        public ActionResult HomePage()
        {
            
            return View();
        }

    }

}