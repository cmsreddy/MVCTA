using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMVC.Models;
using TAMVC.Models.DBModels;
namespace TAMVC.Controllers
{
    [Authorize]
    public class ViewAdsController : Controller
    {
        //
        // GET: /ViewAds/
        TAC_Team6Entities db = new TAC_Team6Entities();
        public static int id=0;
        public ActionResult ViewAds(int? catId,int page=1)
        {
            
            AllAdsModel model = new AllAdsModel();
            Pager p;
            model.catList = db.TAC_Category.ToList();
            if (catId == null && id<1)
            {
                model.claList = db.TAC_Classified.OrderBy(x => x.PostedDate).ToList().Skip(2 * (page - 1)).Take(2).ToList();
                p = new Pager(db.TAC_Classified.ToList().Count(), page);
            }
            else
            {
                if(catId!=null)
                {
                    id = (int)catId;
                }                
                var tempLst= db.TAC_Classified.Where(x => x.CategoryId.Equals(id)).ToList();
                model.claList = tempLst.OrderBy(x => x.PostedDate).ToList().Skip(2 * (page - 1)).Take(2).ToList();
                p = new Pager(tempLst.ToList().Count(), page);
            }
                model.Pager = p;
            return View(model);
        }

    }
}
