using PublicisPOS.Domain.Aggregates;

namespace PublicisPOS.Application.Services.Abstractions
{
    public interface IOrderService
    {
        Task<Order> Save(Order order);
    }
}