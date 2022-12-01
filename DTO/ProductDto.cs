using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class ProductDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public decimal Price { get; set; }
        public string ImageFullPath { set; get; }

     
        public string BrandName { set; get; }
        public string TypeName { set; get; }

        public int ProductTypeId { set; get; }


        public int ProductBrandId { set; get; }
    }
} 
