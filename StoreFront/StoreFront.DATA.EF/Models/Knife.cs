using System;
using System.Collections.Generic;

namespace StoreFront.UI.MVC.Models
{
    public partial class Knife
    {
        public int KnifeId { get; set; }
        public int? KnifeBrandId { get; set; }
        public string KnifeType { get; set; } = null!;
        public decimal KnifePrice { get; set; }
        public int KnifeStatusId { get; set; }
        public bool IsDiscontinued { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual KnifeBrand? KnifeBrand { get; set; }
        public virtual ProductStatus KnifeStatus { get; set; } = null!;
    }
}
