using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace StoreFront.DATA.EF.Models//.Metadata
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category { }
    
    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product { }
    
    [ModelMetadataType(typeof(KnifeMetadata))]
    public partial class Knife { }
    
    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order { }
    
    [ModelMetadataType(typeof(SupplierMetadata))]
    public partial class Supplier { }
    
    [ModelMetadataType(typeof(CustomerMetadata))]
    public partial class Customer { }


}
