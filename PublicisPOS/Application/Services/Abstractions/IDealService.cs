using PublicisPOS.Domain.Entities;
using PublicisPOS.DTO;

namespace PublicisPOS.Application.Services.Abstractions
{
    public interface IDealService
    {
        Task<DealDto> CreateDeal(DealDto deal);
        Task DeleteDeal(int id);
        Task<List<DealDto>> GetAllDeals();
        Task<DealDto> GetDealById(int id);
        Task<DealDto> UpdateDeal(DealDto deal);
    }
}