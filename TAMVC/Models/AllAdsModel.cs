using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAMVC.Models.DBModels;
namespace TAMVC.Models
{
    public class AllAdsModel
    {
        public List<TAC_Category> catList { get; set; }
        public List<TAC_Classified> claList { get; set; }
        public Pager Pager { set; get;}

    }
}