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
    
    public partial class Leave
    {
        public long LeaveId { get; set; }
        public Nullable<System.DateTime> DateOfLeave { get; set; }
        public string Description { get; set; }
        public string LeaveStatus { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public string Location { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
