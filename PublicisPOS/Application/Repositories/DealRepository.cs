using Microsoft.EntityFrameworkCore;
using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Infrastructure;

namespace PublicisPOS.Application.Repositories
{
    public class DealRepository : IDealRepository
    {
        private readonly DealContext _context;

        public DealRepository(DealContext context)
        {
            _context = context;
        }

        public async Task<Deal> CreateDeal(Deal deal)
        {
            _context.Deals.Add(deal);
            await _context.SaveChangesAsync();
            return deal;
        }

        public async Task<Deal> DeleteDeal(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Deal>> GetAllDeals()
        {
            throw new NotImplementedException();
        }

        public async Task<Deal> GetDealById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Deal> GetDealBySKU(int sku)
        {
            return await _context.Deals.SingleOrDefaultAsync(x => x.Sku == sku);
        }

        public Task<Deal> UpdateDeal(Deal deal)
        {
            throw new NotImplementedException();
        }
    }

}
