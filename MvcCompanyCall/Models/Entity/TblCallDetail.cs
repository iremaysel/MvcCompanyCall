//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcCompanyCall.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblCallDetail
    {
        public int ID { get; set; }
        public Nullable<int> DetailCall { get; set; }
        public string DetailStatement { get; set; }
        public Nullable<System.DateTime> DetailDate { get; set; }
        public string DetailClock { get; set; }
    
        public virtual TblCall TblCall { get; set; }
    }
}
