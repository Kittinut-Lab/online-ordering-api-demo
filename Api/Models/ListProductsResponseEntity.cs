using System;
using System.Collections.Generic;

namespace online_ordering_api.Models
{
    public class ListProductsResponseEntity: BaseResponseEntity
    {   
        public List<ProductEntity> Products { get; set; }
    }
}
