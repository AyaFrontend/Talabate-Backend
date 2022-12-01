using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ecommerce.DAL.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Order Recived")]
        OrderRecived,
        [EnumMember(Value = "Order Failed")]
        OrderFailed
    }
}
