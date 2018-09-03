using GoodSupplyWEB.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class SupplierSetProductsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "מחיר")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "כמות")]
        public int Quantity { get; set; }

        [Display(Name = "מחיר יצרן")]
        public Nullable<decimal> ManufacturerPrice { get; set; }

        [Display(Name = "ספק")]
        public int SupplierId { get; set; }

        [Display(Name = "מוצר")]
        public int ManufacturerProductId { get; set; }

        public virtual ManufacturerProducts ManufacturerProducts { get; set; }
        public virtual Suppliers Suppliers { get; set; }
    }
}