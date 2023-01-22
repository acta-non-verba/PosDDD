using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;
using PublicisPOS.DTO;

namespace PublicisPOS.Application.Services
{
    public class DealService : IDealService
    {
        private readonly IDealRepository _dealRepository;
        private readonly IInventoryService _inventoryService;

        public DealService(IDealRepository dealRepository, IInventoryService inventoryService)
        {
            _dealRepository = dealRepository;
            _inventoryService = inventoryService;
        }

        public async Task<DealDto> CreateDeal(DealDto dealDto)
        {
            var deal = new Deal(
                                dealDto.Sku,
                                dealDto.DealType,
                                dealDto.Discount,
                                dealDto.OfferDescription,
                                dealDto.BuyQuantity,
                                dealDto.OfferQuantity,
                                dealDto.Starts,
                                dealDto.Ends
                               );
            // validate deal
            deal.ValidateDeal(deal);
            var savedDeal = await _dealRepository.CreateDeal(deal);
            if (savedDeal.Id > 0)
                return dealDto;
            else
                throw new Exception("deal cannot be created");
        }

        public async Task<List<DealDto>> GetAllDeals()
        {
            List<Deal> deals = await _dealRepository.GetAllDeals();
            return deals.Select(d => new DealDto
            {
                Id = d.Id,
                DealType = d.DealType,
                Discount = d.Discount.Value,
                OfferDescription = d.Offer.Description,
                BuyQuantity = d.Offer.BuyQuantity,
                OfferQuantity = d.Offer.OfferQuantity
            }).ToList();

        }

        public async Task<DealDto> GetDealById(int id)
        {

            Deal deal = await _dealRepository.GetDealById(id);
            return new DealDto
            {
                Id = deal.Id,
                DealType = deal.DealType,
                Discount = deal.Discount.Value,
                OfferDescription = deal.Offer.Description,
                BuyQuantity = deal.Offer.BuyQuantity,
                OfferQuantity = deal.Offer.OfferQuantity
            };
        }

        public async Task<DealDto> UpdateDeal(DealDto dealDto)
        {
            var deal = new Deal(dealDto.Sku,dealDto.DealType, dealDto.Discount, dealDto.OfferDescription,
                dealDto.BuyQuantity, dealDto.OfferQuantity,dealDto.Starts,dealDto.Ends);
            // validate deal
            deal.ValidateDeal(deal);
            var updatedDeal = await _dealRepository.UpdateDeal(deal);
            return dealDto;
        }

        public async Task DeleteDeal(int id)
        {
            await _dealRepository.DeleteDeal(id);
        }
    }

}
