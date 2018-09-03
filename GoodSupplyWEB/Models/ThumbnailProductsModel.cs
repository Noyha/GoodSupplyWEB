using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.Models
{
    public class ThumbnailProductsModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ManufacturerName { get; set; }
        public string CommerciallName { get; set; }
        public string CatalogNumber { get; set; }
        public string ImageUrl { get; set; }
        public int PackageQuantity { get; set; }
        public string Link { get; set; }

        public decimal ProductPrice { get; set; }
    }
}