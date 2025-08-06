using Inventory_Management_System.Dtos.StockInsertDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IStockService
    {
        Task<bool> AddStockFromPurchase(StockInsertDto stockDto);
    }
}
