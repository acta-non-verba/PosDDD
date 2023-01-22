using PublicisPOS.Domain.Aggregates;

namespace PublicisPOS.Application.Services.Abstractions
{
    public interface ISaleService
    {
        void ApplyDeal(Sale sale);
    }
}