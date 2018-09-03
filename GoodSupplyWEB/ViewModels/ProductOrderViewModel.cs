using GoodSupplyWEB.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class ProductOrderViewModel
    {

        public int Id { get; set; }

        //ManufacturerProduct Details
        public int ManufacturerProductId { get; set; }

        [Display(Name = "שם מסחרי")]
        public string CommercialName { get; set; }

        [Display(Name = "מס' קטלוגי")]
        public string CatalogNumber { get; set; }

        [Display(Name = "תמונת מוצר")]
        public string ImageUrl { get; set; }

        [Display(Name = "כמות יח' במארז")]
        public int PackageQuantity { get; set; }

        [Display(Name = "מימדים - רוחב")]
        public Nullable<int> Width { get; set; }

        [Display(Name = "אורך")]
        public Nullable<int> Length { get; set; }

        [Display(Name = "גובה")]
        public Nullable<int> Height { get; set; }

        [Display(Name = "שם יצרן")]
        public int ManufacturerId { get; set; }

        public int ProductId { get; set; }

        public virtual Manufacturers Manufacturers { get; set; }
        public virtual Products Products { get; set; }

        //Product Details
        [Display(Name = "שם מוצר")]
        public string ProductName { get; set; }

        //Manufacturer Details
        [Display(Name = "שם יצרן")]
        public string ManufacturerName { get; set; }



        //OrderDetails Details
        public int OederDetailsId { get; set; }

        [Display(Name = "כמות")]
        public Nullable<int> Quantity { get; set; }

        [Display(Name = "מחיר לאחר כמות")]
        public Nullable<decimal> QuantityPrice { get; set; }

        [Display(Name = "מחיר ליחידה")]
        public Nullable<double> ItemPrice { get; set; }

        [Display(Name = "מחיר כולל של ההזמנה")]
        public Nullable<decimal> TotalOrderPrice { get; set; }

        public int OrderId { get; set; }
        //public int ManufacturerProductId { get; set; }

        public virtual Orders Orders { get; set; }
        public virtual ManufacturerProducts ManufacturerProducts { get; set; }


        //User Details
        public int SupplierId { get; set; }
        public int ClientId { get; set; }

        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Display(Name = "שם משפחה")]
        public string LastName { get; set; }

        [Display(Name = "שם")]
        public string FullName { get { return FirstName + " " + LastName; } }

        [Display(Name = "שם העסק")]
        public string BusinessName { get; set; }


        //Prices Details

        public List<Prices> PricesList { get; set; }

        public Prices Prices { get; set; }

        public int PriceId { get; set; }

        [Display(Name = "מחיר")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "כמות")]
        public int SupplierQuantity { get; set; }

        [Display(Name = "מחיר יצרן")]
        public Nullable<decimal> ManufacturerPrice { get; set; }

        //public int SupplierId { get; set; }
        //public int ManufacturerProductId { get; set; }

        //public virtual ManufacturerProducts ManufacturerProducts { get; set; }
        public virtual Suppliers SuppliersCheck { get; set; }

        public IEnumerable<Suppliers> Suppliers { get; set; }

        public IEnumerable<Clients> Clients { get; set; }
    }
}