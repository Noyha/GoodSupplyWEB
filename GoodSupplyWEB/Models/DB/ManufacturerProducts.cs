//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoodSupplyWEB.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ManufacturerProducts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ManufacturerProducts()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
            this.Prices = new HashSet<Prices>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string CatalogNumber { get; set; }
        public string ImageUrl { get; set; }
        public int PackageQuantity { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Length { get; set; }
        public Nullable<int> Height { get; set; }
        public int ManufacturerId { get; set; }
        public int ProductId { get; set; }
    
        public virtual Manufacturers Manufacturers { get; set; }
        public virtual Products Products { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prices> Prices { get; set; }
    }
}