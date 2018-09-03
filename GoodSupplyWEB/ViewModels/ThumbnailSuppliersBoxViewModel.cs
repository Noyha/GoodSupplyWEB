using GoodSupplyWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class ThumbnailSuppliersBoxViewModel
    {
        public IEnumerable<ThumbnailSuppliersModel> SuppliersThumbnails { get; set; }
    }
}