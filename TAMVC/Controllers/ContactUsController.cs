using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMVC.Models.DBModels;

namespace TAMVC.Controllers
{
    public class ContactUsController : Controller
    {
        //
        // GET: /ContactUs/

        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(TAC_ContactUs model)
        {
           TAC_Team6Entities db = new TAC_Team6Entities();
            db.TAC_ContactUs.Add(model);
           var flag=db.SaveChanges();
           if (flag > 0)
           {
               ViewBag.ContactMessage = "Thanks for contacting us..We will get back to you shortly";
               model = null;
           }
           return View(model);
        }
    }
}
