using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcCompanyCall.Models.Entity;

namespace MvcCompanyCall.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DbJobTrackingEntities db = new DbJobTrackingEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(TblCompanies p)
        {
            var informations = db.TblCompanies.FirstOrDefault(x => x.Email == p.Email && x.Password == p.Password);
            if (informations != null)
            {
                FormsAuthentication.SetAuthCookie(informations.Email, false);
                Session["Email"] = informations.Email.ToString();
                return RedirectToAction("ActiveCalls", "Default");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}