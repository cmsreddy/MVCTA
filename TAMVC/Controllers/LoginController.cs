using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMVC.DAL;
using TAMVC.Models;

namespace TAMVC.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel l)
        {
            SQLHelper s = new SQLHelper();
            if (s.Login(l.Email, l.Password))
            {
             Response.Redirect("/home");
            }
          
                ViewBag.ErrorMessage = "Login Failed";
                return View();
            
        }

    }
}
