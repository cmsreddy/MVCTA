//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TAMVC.Models.DBModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class TAC_ClassifiedContact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactCity { get; set; }
        public int ClassifiedId { get; set; }
    
        public virtual TAC_Classified TAC_Classified { get; set; }
    }
}
