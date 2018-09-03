using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.Models
{
    public class ThumbnailSupplierProductsModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ManufacturerName { get; set; }
        public string CommerciallName { get; set; }
        public string CatalogNumber { get; set; }
        public string ImageUrl { get; set; }
        public int PackageQuantity { get; set; }
        public string Link { get; set; }

        public int PriceId { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ManufacturerPrice { get; set; }
        public int SupplierId { get; set; }
    }
}