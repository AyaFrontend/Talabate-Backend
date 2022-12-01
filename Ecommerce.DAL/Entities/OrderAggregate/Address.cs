using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Entities.OrderAggregate
{
    public class Address
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Country { set; get; }
        public string City { set; get; }
        public string Street { set; get; }
    }
}
