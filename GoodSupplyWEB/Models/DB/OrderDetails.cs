//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoodSupplyWEB.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetails
    {
        public int Id { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> QuantityPrice { get; set; }
        public Nullable<double> ItemPrice { get; set; }
        public Nullable<decimal> TotalOrderPrice { get; set; }
        public int OrderId { get; set; }
        public int ManufacturerProductId { get; set; }
    
        public virtual ManufacturerProducts ManufacturerProducts { get; set; }
        public virtual Orders Orders { get; set; }
    }
}