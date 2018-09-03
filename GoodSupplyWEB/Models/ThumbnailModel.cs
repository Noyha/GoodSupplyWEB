using GoodSupplyWEB.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.Models
{
    public class ThumbnailModel
    {
        public int ManufacturerProductId { get; set; }

        //public decimal ProductPrice { get; set; } //Supplier fills in
        //public int Quantity { get; set; } //Supplier fills in
        //public Nullable<decimal> ManufacturerPrice { get; set; } //Supplier fills in

        public string CatalogNumber { get; set; } //ManufacturerProducts
        public string ImageUrl { get; set; } //ManufacturerProducts
        public int PackageQuantity { get; set; } //ManufacturerProducts

        public string ProductName { get; set; } //Products
        public IEnumerable<Products> Products { get; set; }

        public string ManufacturerName { get; set; } //Manufacturers
        public IEnumerable<Manufacturers> Manufacturers { get; set; }

        //public Boolean Checked { get; set; }

        public IEnumerable<Suppliers> Suppliers { get; set; }
        public IEnumerable<ManufacturerProducts> ManufacturerProducts { get; set; }

    }
}