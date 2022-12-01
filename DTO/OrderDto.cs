using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class OrderDto
    {
        public int deliveryMethodId { set; get; }
        public string BasketId { set; get; }
        public AddressDto addressToship { set; get; }
    }
}
