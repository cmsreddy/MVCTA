using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAMVC.Models
{
    public class Classified
    {

        public string ClassifiedId { get; set; }
        public string ClassifiedTitle { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ClassifiedPrice { get; set; }
        public string ClassifiedImage { get; set; }
        public string PostedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CategoryName { get; set; }
    }
}