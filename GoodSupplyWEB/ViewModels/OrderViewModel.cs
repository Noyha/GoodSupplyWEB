using GoodSupplyWEB.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<Suppliers> Suppliers { get; set; }
        public IEnumerable<Clients> Clients { get; set; }
        public Orders Orders { get; set; }
    }
}