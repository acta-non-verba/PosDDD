using PublicisPOS.Domain.Entities;

namespace PublicisPOS.Application.Repositories.Abstractions
{
    public interface IDealRepository
    {
        Task<Deal> CreateDeal(Deal deal);
        Task<Deal> DeleteDeal(int id);
        Task<List<Deal>> GetAllDeals();
        Task<Deal> GetDealById(int id);
        Task<Deal> GetDealBySKU(int sku);
        Task<Deal> UpdateDeal(Deal deal);
    }
}
