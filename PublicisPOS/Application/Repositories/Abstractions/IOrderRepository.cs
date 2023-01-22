using PublicisPOS.Domain.Aggregates;

namespace PublicisPOS.Application.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(int id);
        Task<Order> Save(Order order);
    }

}
