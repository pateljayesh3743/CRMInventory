//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRMInventory.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class purchase_details
    {
        public int purchase_details1 { get; set; }
        public Nullable<int> purchase_id { get; set; }
        public Nullable<int> sr_no { get; set; }
        public Nullable<int> product_id { get; set; }
        public string alternate_qty { get; set; }
        public Nullable<decimal> gross_weight { get; set; }
        public Nullable<decimal> less { get; set; }
        public string net_weight { get; set; }
        public Nullable<decimal> rate { get; set; }
        public string per { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> created_on { get; set; }
        public Nullable<int> company_id { get; set; }
        public Nullable<int> status { get; set; }
    }
}
