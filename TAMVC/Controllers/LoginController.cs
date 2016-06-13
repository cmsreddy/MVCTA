using Microsoft.AspNet.Membership.OpenAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TAMVC.DAL;
using TAMVC.Models;
using TAMVC.Models.DBModels;

namespace TAMVC.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        TAC_Team6Entities db = new TAC_Team6Entities();
        // GET: Login
        public ActionResult Login()
        {
            return View(checkCookie());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
           //var url= HttpContext.Request.RawUrl;
            if (ModelState.IsValid)
            {
                var list = db.TAC_User.ToList();
                if(list.Where(x=>x.Email.Equals(model.Email)).Count()==0)
                {
                    model.ErrorMessage="Email doesn't exist...Please register";
                    return View(model);
                }
                else if ((list.Where(
                    x => x.Email.Equals(model.Email) &&
                    x.UPassword.Equals(model.UPassword) &&
                    x.IsActive.Equals(true) &&
                    x.IsVerified.Equals(true))).Count() > 0)
                {
                    TAC_User user = list.Where(x => x.Email.Equals(model.Email) &&
                    x.UPassword.Equals(model.UPassword)).FirstOrDefault();
                    System.Web.HttpContext.Current.Session["userDetails"] = user;
                    // Session.Timeout = 2;
                    HttpCookie Email = new HttpCookie("email");
                    HttpCookie Password = new HttpCookie("password");
                    if (model.RememberMe)
                    {
                        Email.Expires = DateTime.Now.AddSeconds(3600);
                        Email.Value = model.Email;
                        Response.Cookies.Add(Email);
                        Password.Expires = DateTime.Now.AddSeconds(3600);
                        Password.Value = model.UPassword;
                        Response.Cookies.Add(Password);
                    }
                    else
                    {
                        if (Response.Cookies["email"] != null)
                        {
                            Email.Expires = DateTime.Now.AddDays(-1D);
                            Response.Cookies.Add(Email);
                        }

                        if (Response.Cookies["password"] != null)
                        {
                            Password.Expires = DateTime.Now.AddDays(-1D);
                            Response.Cookies.Add(Password);
                        }
                    }
                    //Response.Redirect("/MyAccount/Index");
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, model.Email, DateTime.Now, DateTime.Now.AddMinutes(5), false, "user");
                    HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(tkt));
                    Response.Cookies.Add(cookie1);
                    if (Request.QueryString["ReturnUrl"] == null)
                    {
                        Response.Redirect("/MyAccount/Index");
                    }
                    else
                    {
                        RedirectToAction("Login", "Login");
                    }                                   
                }
                else if(list.Where(x=>x.IsLocked.Equals(true)).Count()>0){
                    model.ErrorMessage = "Your account is temporarily locked";
                }
                else
                {
                    var flag = 0;
                    int i = 0, j = 0;
                    
                    for (i = 0; i < Request.Cookies.Count; i++)
                    {
                        var cookie = Request.Cookies["Locked" + i];
                        if (model != null && cookie != null)
                        {
                            if (cookie["email"].ToString() == model.Email)
                            {
                                flag = 1;
                                if (int.Parse(cookie["count"]) > 3)
                                {
                                        cookie["count"] = "0";
                                        model.ErrorMessage = "Your account is temporarily locked";
                                        var id = list.Where(x => x.Email == model.Email).First().UserId;
                                        var original = db.TAC_User.Find(id);
                                        original.IsLocked = true;
                                        db.SaveChanges();
                                        Response.Cookies["Locked"+i].Expires=DateTime.Now.AddDays(-1D);
                                        Response.Cookies.Add(cookie);
                                        return View(model);                                  
                                }
                                cookie["count"] = (int.Parse(cookie["count"]) + 1).ToString();
                                Response.Cookies.Set(cookie);
                            }
                            j = i;
                        }
                    }
                    if (flag == 0)
                    {
                        HttpCookie chkLocks = new HttpCookie("Locked" + ++j);
                        chkLocks["email"] = model.Email;
                        chkLocks["count"] = "0";
                        chkLocks.Expires = DateTime.Now.AddSeconds(3600);
                        Response.Cookies.Add(chkLocks);
                    }
                    model.ErrorMessage = "Please enter correct credentials";
                }
            }
            return View(model);
        }
        
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["userDetails"] = null;
            return RedirectToAction("Login");
        }
        public LoginModel checkCookie()
        {
            LoginModel l = null;
            string email = string.Empty;
            string password = string.Empty;
            if (Request.Cookies["email"] != null)
                email = Request.Cookies["email"].Value;
            if (Request.Cookies["password"] != null)
                password = Request.Cookies["password"].Value;
            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
                l = new LoginModel { Email = email, UPassword = password };
            return l;
        }
        public IEnumerable<ProviderDetails> GetProviderNames()
        {
            return OpenAuth.AuthenticationClients.GetAll();
        }
    }
}
