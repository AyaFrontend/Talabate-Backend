using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class OrderItemReturnDto
    {
        public decimal Price { set; get; }
        public int Quantatiy { set; get; }
        public int ProductId { set; get; }
        public string UrlPic { set; get; }
        public string ProductName { set; get; }
    }
}
