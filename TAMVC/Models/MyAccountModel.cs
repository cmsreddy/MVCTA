using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAMVC.Models.DBModels;

namespace TAMVC.Models
{
    public class MyAccountModel
    {
        public IEnumerable<TAC_Classified> lst { get; set; }

        public Pager Pager { get; set; }
    }
}