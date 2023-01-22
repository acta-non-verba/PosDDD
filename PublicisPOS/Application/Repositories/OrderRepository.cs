using Microsoft.EntityFrameworkCore;
using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Domain.Aggregates;
using PublicisPOS.Infrastructure;

namespace PublicisPOS.Application.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.Include(x => x.OrderItems).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order> Save(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
