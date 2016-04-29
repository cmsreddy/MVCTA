using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMVC.DAL;
using TAMVC.Models;

namespace TAMVC.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel r)
        {
            SQLHelper s = new SQLHelper();
            var res=s.registerUser(r.Email, r.Password);
            if (res)
            {
                ViewBag.ErrorMsg = "Registration Successful";
                return View();
            }
            else
            {
                ViewBag.ErrorMsg= "User Already Exists";
                return View();
            }
        }
    }
}
