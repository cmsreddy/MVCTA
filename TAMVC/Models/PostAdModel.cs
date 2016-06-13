using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAMVC.Models.DBModels;

namespace TAMVC.Models
{
    public class PostAdModel:Classified
    {
        public TAC_ClassifiedContact User { get; set; }
        public TAC_Classified Classified { get; set; }
        public TAC_Category Category { get; set; }
        public HttpPostedFileBase fileupload { get; set; }
    }
}