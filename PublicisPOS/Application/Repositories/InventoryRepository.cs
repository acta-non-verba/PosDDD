using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;
using PublicisPOS.Infrastructure;
using System;

namespace PublicisPOS.Application.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryContext _context;

        public InventoryRepository(InventoryContext context)
        {
            _context = context;
            using (_context)
            {
                var inventoryItems = new List<InventoryItem>{
                new InventoryItem{Id=1,Sku=453,Item="Apples",Quantity=new Quantity(2,Unit.Kilogram),Price=75},
                new InventoryItem{Id=1,Sku=799,Item="Pair of hair tie",Quantity=new Quantity(1,Unit.Number),Price=15},
                new InventoryItem{Id=1,Sku=799,Item="Amul butter",Quantity=new Quantity(2,Unit.Gram),Price=20}
                };
                _context.InventoryItems.AddRange(inventoryItems);
                _context.SaveChanges();
            }
        }

        public InventoryItem GetBySKU(int Sku)
        {
            return _context.InventoryItems.FirstOrDefault(i => i.Sku == Sku);
        }
    }

}
