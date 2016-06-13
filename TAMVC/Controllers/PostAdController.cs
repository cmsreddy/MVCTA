using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMVC.Models;
using TAMVC.DAL;
using System.Web.Security;
using TAMVC.Models.DBModels;
using System.IO;

namespace TAMVC.Controllers
{

    [Authorize]
    public class PostAdController : Controller
    {
        private TAC_Team6Entities db = new TAC_Team6Entities();

        /// <summary>
        /// Get Method for Post Ad
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostAd(PostAdModel model)
        {
            SQLHelper Catageories = new SQLHelper();
            model.CList = Catageories.ReadCatagory();
            model.CId = "1";
            return View(model);
        }

        /// <summary>
        /// Post Method for Posting Ad's
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PostAd(PostAdModel model, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SQLHelper Userguid = new SQLHelper();
                    model.Classified.Summary = model.Classified.Description;
                    model.Classified.PostedDate = DateTime.Now;
                    model.Classified.CategoryId = Convert.ToInt32(model.CId);
                    TAC_User userTable = new TAC_User();
                    String username = System.Web.HttpContext.Current.User.Identity.Name;
                    model.Classified.CreatedBy = new Guid(Userguid.UserGUID(username));
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];

                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("../UploadedImages"), fileName);
                            file.SaveAs(path);
                            model.Classified.ClassifiedImage = Path.Combine("../UploadedImages", fileName);
                        }
                    }
                    db.TAC_Classified.Add(model.Classified);
                    db.SaveChanges();
                    model.User.ClassifiedId = model.Classified.ClassifiedId;
                    model.User.ContactPhone = model.User.ContactPhone;
                    model.User.ContactCity = model.User.ContactCity;
                    db.TAC_ClassifiedContact.Add(model.User);
                    db.SaveChanges();
                    return RedirectToAction("Index", "MyAccount");
                }
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public ActionResult DetailsPage(int? classifiedId)
        {
            SQLHelper some = new SQLHelper();
            DetailsModel classified = some.GetClassifiedById((int.Parse(classifiedId.ToString()) != 0) ? int.Parse(classifiedId.ToString()) : 1);
            if (classified != null)
            {
                return View(classified);
            }
            else return RedirectToAction("Index", "MyAccount");
        }
        public ActionResult GetCategories()
        {
           // TAC_Category model = new TAC_Category();
            var list = db.TAC_Category.ToList();
            return View("_CategoriesList",list);
        }

    }
}
