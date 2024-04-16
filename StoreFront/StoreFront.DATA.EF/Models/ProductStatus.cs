using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class ProductStatus
    {
        public ProductStatus()
        {
            Products = new HashSet<Product>();
        }

        public string? ProductStatusName { get; set; }
        public int ProductStatusId { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
