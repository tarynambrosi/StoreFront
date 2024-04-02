using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Category
    {
        public Category()
        {
            Knives = new HashSet<Knife>();
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }

        public virtual ICollection<Knife> Knives { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
