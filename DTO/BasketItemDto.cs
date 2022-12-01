using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class BasketItemDto
    {
        public int Id { set; get; }
        [Required]
        public string ProductName { set; get; }


        [Required]
        public string BrandName { set; get; }

        [Required]
        public string TypeName { set; get; }

        [Required]
        [Range(0.1 , double.MaxValue)]
        public decimal Price { set; get; }


        [Required]
        [Range(1,int.MaxValue)]
        public int Quantity { set; get; }
    }
}
