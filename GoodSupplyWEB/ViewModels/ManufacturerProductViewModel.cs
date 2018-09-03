using GoodSupplyWEB.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class ManufacturerProductViewModel
    {
        public IEnumerable<Manufacturers> Manufacturers { get; set; }
        public IEnumerable<Products> Products { get; set; }
        public ManufacturerProducts ManufacturerProducts { get; set; }
    }
}