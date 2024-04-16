using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreFront.DATA.EF.Models//.Metadata
{
    public class CategoryMetadata
    {
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category")]
        [StringLength(150)]
        public string CategoryName { get; set; } = null!;

        [Display(Name = "Description")]
        [StringLength(500)]
        public string? CategoryDescription { get; set; }
    }

    public class ProductMetadata
    {
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Product")]
        [StringLength(200)]
        public string ProductName { get; set; } = null!;

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [Display(Name = "Price")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Description")]
        [StringLength(500)]
        public string? ProductDescription { get; set; }


        public int ProductStatusId { get; set; }

        [Display(Name = "Discontinued?")]
        public bool IsDiscontinued { get; set; }

        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }

        [StringLength(75)]
        [Display(Name = "Image")]
        public string? ProductImage { get; set; }
    }

    public class OrderMetadata
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; } = null!;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Order Date")]
        [Required]
        public DateTime OrderDate { get; set; }

        [StringLength(100)]
        [Display(Name = "Ship To")]
        [Required]
        public string ShipToName { get; set; } = null!;

        [StringLength(50)]
        [Display(Name = "City")]
        [Required]
        public string ShipCity { get; set; } = null!;

        [StringLength(2)]
        [Display(Name = "State")]
        public string? ShipState { get; set; }

        [StringLength(5)]
        [Display(Name = "Zip")]
        [Required]
        [DataType(DataType.PostalCode)]
        public string ShipZip { get; set; } = null!;
    }

    public class SupplierMetadata
    {
        public int SupplierId { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        [StringLength(100)]
        public string SupplierName { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = null!;

        [StringLength(2)]
        public string? State { get; set; }

        [StringLength(5)]
        [DataType(DataType.PostalCode)]
        public string? Zip { get; set; }

        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }

    public class CustomerMetadata
    {
        public string CustomerId { get; set; } = null!;

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(150)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(2)]
        [DataType(DataType.Text)]
        public string? State { get; set; }

        [StringLength(5)]
        [DataType(DataType.PostalCode)]
        public string? Zip { get; set; }

        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }
}