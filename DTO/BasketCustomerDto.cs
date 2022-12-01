using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class BasketCustomerDto
    {
        [Required]
        public string Id { set; get; }

        public List<BasketItemDto> items {set;get;} = new List<BasketItemDto>();
    }
}
