using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMVC.Models;
using TAMVC.DAL;
using TAMVC.Models.DBModels;

namespace TAMVC.Controllers
{
    
    public class MyAccountController : Controller
    {
        //
        // GET: /MyAccount/

        [HttpGet]
        public ActionResult Index(int page=1)
        {
            TAC_Team6Entities db = new TAC_Team6Entities();
            var list =db.TAC_Classified.ToList();
            TAC_User u = (TAC_User)Session["userDetails"] as TAC_User;           
            var newLst = list.Where((x =>  x.CreatedBy == u.UserId));
            Pager p = new Pager(newLst.Count(), page);
           var model = new MyAccountModel
           {
               lst=newLst.Skip(2*(page-1)).Take(2),
               Pager = p
           };
            return View("MyAccount",model);

        }


    }
}
