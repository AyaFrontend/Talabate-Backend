using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Entities.OrderAggregate;
using Ecoomerce.BLL.Interfaces;
using Ecoomerce.BLL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRedisRepository<CustomerBasket> _redisRepo;
        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        //private readonly IGenericRepository<Order> _orderRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        public OrderService(IUnitOfWork unitOfWork , IRedisRepository<CustomerBasket> redisRepo , IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _redisRepo = redisRepo;
            _paymentService = paymentService;
        }
        //public OrderService(IRedisRepository<CustomerBasket> redisRepo,
        //    IGenericRepository<Product> productRepo ,
        //    IGenericRepository<DeliveryMethod> deliveryMethodRepo,
        //    IGenericRepository<Order> orderRepo)
        //{
        //    _redisRepo = redisRepo;
        //    _productRepo = productRepo;
        //    _deliveryMethodRepo = deliveryMethodRepo;
        //    _orderRepo = orderRepo;
            
        //}

        public async Task<Order> CreateOrder(int deliveryMethodId, Address ShipToAddress, string basketId, string BuyerEmail)
        {
            //Get Basket that user will order it
            var basket = await _redisRepo.GetAsync(basketId);

            //get products from database عشان انالا اثق فى السعر اللى فى الباسكت بتاع العميل احسن يكون مغيره
            var orderedItems = new List<OrderItem>();
            
            foreach (var item in basket.items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var productItemOrdered= new ProductItemOrdered(product.Id , product.PictureUrl, product.Name);
                var orderItem = new OrderItem(product.Price, item.Quantity, productItemOrdered);
                orderedItems.Add(orderItem);
            }
            //get delivery method
            var delivery = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            var deliveryMethod = new DeliveryMethod(delivery.ShortName, delivery.Description, delivery.DeliveryTime, delivery.Cost);
            var subTotal = orderedItems.Sum(item => item.Price * item.Quantatiy) ;

          //  var order = new Order(BuyerEmail,ShipToAddress,deliveryMethod, orderedItems,subTotal , basket.PaymentIntentId);
            var spec = new OrderwithPaymentIntentIdSpec(basket.PaymentIntentId);
            var existedOrder = await _unitOfWork.Repository<Order>().GetByIdSpec(spec);
            if(existedOrder != null)
            {
                await _unitOfWork.Repository<Order>().DeleteAsync(existedOrder);
                await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            }
            var order = new Order(BuyerEmail, ShipToAddress, deliveryMethod, orderedItems, subTotal, basket.PaymentIntentId);
            await _unitOfWork.Repository<Order>().AddAsync(order);
            //save to database [TODOLIST]
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

        public async Task<IReadOnlyList<Order>> GetAllOrdersByUserEmailAsync(string userEmail)
        {
            var spec = new OrderSpecificationWithDeliveryMethodAndItems(userEmail);
            return await _unitOfWork.Repository<Order>().GetAllSpec(spec);
        }

        public async Task<Order> GetOrderByUserEmailAsync(int orderId, string userEmail)
        {
            var spec = new OrderSpecificationWithDeliveryMethodAndItems(orderId, userEmail);
            return await _unitOfWork.Repository<Order>().GetByIdSpec(spec);
        }
    }
}
