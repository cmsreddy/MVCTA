using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMVC.Models;
using TAMVC.Models.DBModels;
using System.IO;
namespace TAMVC.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index(int page = 1)
        {
            TAC_Team6Entities db = new TAC_Team6Entities();
            var list = db.TAC_Classified.ToList();
            Pager p = new Pager(list.Count(), page);
            var model = new MyAccountModel
            {
                lst = list.Skip(2 * (page - 1)).Take(2),
                Pager = p
            };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
