using System;
using System.Collections.Generic;

namespace StoreFront.UI.MVC.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductStatusId { get; set; }
        public bool IsDiscontinued { get; set; }
        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public string? ProductImage { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ProductStatus ProductStatus { get; set; } = null!;
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
