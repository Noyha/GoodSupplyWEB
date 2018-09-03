using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.Models
{
    public class ThumbnailSuppliersModel
    {
        public int SupplierId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress { get; set; }
        public string VATIdNumber { get; set; }
        public string Link1 { get; set; }
        public string Link2 { get; set; }
    }
}