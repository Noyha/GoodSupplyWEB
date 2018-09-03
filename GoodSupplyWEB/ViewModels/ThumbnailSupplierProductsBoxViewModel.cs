using GoodSupplyWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class ThumbnailSupplierProductsBoxViewModel
    {
        public IEnumerable<ThumbnailSupplierProductsModel> SupplierProductsThumbnails { get; set; }
    }
}