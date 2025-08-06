using IMS.Application.ServiceInterface;
using Inventory_Management_System.Dtos.StockInsertDto;
using Inventory_Management_System.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockrepository;
        public StockService(IStockRepository stockrepository)
        {
            _stockrepository = stockrepository;
        }
       public async Task<bool> AddStockFromPurchase(StockInsertDto stockDto)
       {
          bool result =await _stockrepository.AddStockFromPurchase(stockDto);
            return result;
       }
    }
}
