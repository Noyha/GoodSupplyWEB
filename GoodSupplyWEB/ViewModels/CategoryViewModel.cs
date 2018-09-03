using GoodSupplyWEB.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class CategoryViewModel
    {
        public IEnumerable<Categories> CategoriesList { get; set; }
        public Categories Categories { get; set; }
    }
}