using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TAMVC.DAL;
using TAMVC.Models;
using TAMVC.Models.DBModels;

namespace TAMVC.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        TAC_Team6Entities db = new TAC_Team6Entities();

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(TAC_User r)
        {
            bool flag = (from user in db.TAC_User
                         where user.Email.Equals(r.Email)
                         select user).Count() > 0 ? false : true;

            if (flag)
            {
                r.UserId = Guid.NewGuid();
                r.CreatedDate = DateTime.Now;
                db.TAC_User.Add(r);
                db.SaveChanges();
                ViewBag.Message = "Registration Successful";
                SendEmail(r);
                return View();
            }
            else
            {
                ViewBag.Message = "User Already Exists";
                return View();
            }
        }

        public void SendEmail(TAC_User model)
        {
            if (ModelState.IsValid)
            {
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(model.Email));
                message.From = new MailAddress("anjania@techaspect.com");
                message.Subject = "Verification Email";
                string linkMessage = "You have registered to TechAspect Classifieds. Please click the below link to verify your account.<br/><br/>";
                string link = "<a href=\"http://localhost:50337/Register/Verified?email=\"" + model.Email  + ">Click ME</a>";
                message.Body = string.Format("Hello " + model.First_Name + " " + model.Last_Name + ",<br/><br/><br/>" + linkMessage + link);
                message.Priority = MailPriority.High;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("192.168.2.210");
                smtp.Send(message);
            }
        }
        public ActionResult Verified(string email)
        {
            Guid id = db.TAC_User.ToList().Where(x => x.Email.Equals(email)).FirstOrDefault().UserId;
            var record = db.TAC_User.Find(id);
            record.IsVerified = true;
            db.Entry(record).Property(e => e.IsVerified).IsModified = true;
            db.SaveChanges();
            return RedirectToAction("Login", "Login");
        }
    }
}
