//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transac
    {
        public long TransacId { get; set; }
        public Nullable<int> Qty { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> DateTransac { get; set; }
        public string TypeOfTransac { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UserRecorder { get; set; }
        public string UserRequest { get; set; }
        public string SuplierName { get; set; }
        public string ZoneName { get; set; }
        public string RegionName { get; set; }
    }
}
