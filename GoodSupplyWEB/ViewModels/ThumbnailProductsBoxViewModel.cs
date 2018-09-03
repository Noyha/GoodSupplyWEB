using GoodSupplyWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class ThumbnailProductsBoxViewModel
    {
        public IEnumerable<ThumbnailProductsModel> ProductsThumbnails { get; set; }
    }
}