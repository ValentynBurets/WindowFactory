//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowFactory.DBLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupplyItem
    {
        public int Id { get; set; }
        public Nullable<int> MaterialId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> СompletionDate { get; set; }
        public Nullable<int> OfferId { get; set; }
        public Nullable<int> SypplyOrderId { get; set; }
    
        public virtual Material Material { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual SupplyOrder SupplyOrder { get; set; }
    }
}
