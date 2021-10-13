//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFClinic
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public Appointment()
        {
            this.Medical_Record = new HashSet<Medical_Record>();
        }
    
        public int id { get; set; }
        public int id_patient { get; set; }
        public int id_user { get; set; }
        public System.DateTime date { get; set; }
        public System.TimeSpan start_hour { get; set; }
        public System.TimeSpan end_hour { get; set; }
        public Nullable<System.TimeSpan> arrival_hour { get; set; }
        public Nullable<System.TimeSpan> departure_hour { get; set; }
        public string state { get; set; }
        public System.DateTime created_at { get; set; }
        public bool active { get; set; }
    
        public virtual Patient Patient { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Medical_Record> Medical_Record { get; set; }
    }
}
