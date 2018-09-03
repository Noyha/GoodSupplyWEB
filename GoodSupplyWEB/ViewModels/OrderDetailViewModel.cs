using GoodSupplyWEB.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class OrderDetailViewModel
    {
        public IEnumerable<Orders> Orders { get; set; }
        public IEnumerable<ManufacturerProducts> ManufacturerProducts { get; set; }
        public OrderDetails OrderDetails { get; set; }
    }
}