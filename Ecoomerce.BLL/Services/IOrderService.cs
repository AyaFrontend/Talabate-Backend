using Ecommerce.DAL.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Services
{
    public interface IOrderService
    {
        public Task<Order> CreateOrder(int deliveryMethodId,Address ShipToAddress ,string basketId, string BuyerEmail);
        public Task<IReadOnlyList<Order>> GetAllOrdersByUserEmailAsync(string userEmail);
        public Task<Order> GetOrderByUserEmailAsync(int orderId, string userEmail);
        public Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();
    
    }
}
