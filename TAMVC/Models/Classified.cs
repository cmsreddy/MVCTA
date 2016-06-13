using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TAMVC.Models
{
    public class Classified
    {
        public Pager Pager { get; set; }
        public int ClassifiedId { get; set; }
        public string CId { get; set; }
        public List<CatagoryList> CList { get; set; }
        
    }
    public class CatagoryList
    {
        public int CategoryID { get; set; }
        public string CategoryNames { get; set; }
        public string CategoryImagesSource { get; set; }

    }
}