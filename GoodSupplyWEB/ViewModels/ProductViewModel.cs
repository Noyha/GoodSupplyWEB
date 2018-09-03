using GoodSupplyWEB.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Categories> Categories { get; set; }
        public Products Products { get; set; }
    }
}