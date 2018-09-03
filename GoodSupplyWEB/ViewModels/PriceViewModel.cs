using GoodSupplyWEB.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class PriceViewModel
    {
        public IEnumerable<Suppliers> Suppliers { get; set; }
        public IEnumerable<ManufacturerProducts> ManufacturerProducts { get; set; }
        public Prices Prices { get; set; }

        //public ManufacturerProducts MP { get; set; }
    }
}