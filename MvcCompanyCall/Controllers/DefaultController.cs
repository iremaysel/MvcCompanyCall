using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            var activeCalls = db.TblCall.Where(x => x.CallCompany == id && x.CallStatus == true).Count();
            ViewBag.active = activeCalls;
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
            var email = (string)Session["Email"];
            var id = db.TblCompanies.Where(x => x.Email == email).Select(y => y.ID).FirstOrDefault();
            var sumCall = db.TblCall.Where(x => x.CallCompany == id).Count();
            var activeCalls = db.TblCall.Where(x => x.CallCompany == id && x.CallStatus == true).Count();
            var passiveCalls = db.TblCall.Where(x => x.CallCompany == id && x.CallStatus == false).Count();
            var authorizedCompany = db.TblCompanies.Where(x => x.ID == id).Select(y => y.Authorized).FirstOrDefault();
            var sectorCompany = db.TblCompanies.Where(x => x.ID == id).Select(y => y.Sector).FirstOrDefault();
            var companyName = db.TblCompanies.Where(x => x.ID == id).Select(y => y.Name).FirstOrDefault();
            var imageCompany = db.TblCompanies.Where(x => x.ID == id).Select(y => y.Image).FirstOrDefault();
            ViewBag.sum = sumCall;
            ViewBag.active = activeCalls;
            ViewBag.passive = passiveCalls;
            ViewBag.authorized = authorizedCompany;
            ViewBag.sector = sectorCompany;
            ViewBag.company = companyName;
            ViewBag.image = imageCompany;
            return View();
        }

        public PartialViewResult PartialMessageTable()
        {
            var email = (string)Session["Email"];
            var id = db.TblCompanies.Where(x => x.Email == email).Select(y => y.ID).FirstOrDefault();
            var messageCount = db.TblMessage.Where(x => x.Receiver == id && x.Status == true).Count();
            var message = db.TblMessage.Where(x => x.Receiver == id && x.Status == true).ToList();
            ViewBag.message = messageCount;
            return PartialView(message);
        }

        public PartialViewResult PartialNotificationTable()
        {
            var email = (string)Session["Email"];
            var id = db.TblCompanies.Where(x => x.Email == email).Select(y => y.ID).FirstOrDefault();
            var activeCalls = db.TblCall.Where(x => x.CallCompany == id && x.CallStatus == true).Count();
            var calls = db.TblCall.Where(x => x.CallCompany == id && x.CallStatus == true).ToList();
            ViewBag.active = activeCalls;
            return PartialView(calls);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }

        public PartialViewResult PartialAccordion()
        {
            
            return PartialView();
        }


    }

}