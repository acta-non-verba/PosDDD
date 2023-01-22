using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.Aggregates;

namespace PublicisPOS.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            
        }

        public async Task<Order> Save(Order order)
        {
            order.ValidateOrder();
            var savedOrder = await _orderRepository.Save(order);
            return savedOrder;
        }
    }

}
