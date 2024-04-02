using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class KnifeBrand
    {
        public KnifeBrand()
        {
            Knives = new HashSet<Knife>();
        }

        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Knife> Knives { get; set; }
    }
}
