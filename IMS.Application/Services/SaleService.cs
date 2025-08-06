using IMS.Application.ServiceInterface;
using Inventory_Management_System.Dtos.Sale;
using Inventory_Management_System.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _salerepository;
        public SaleService(ISaleRepository salerepository)
        {
            _salerepository = salerepository;
        }
        public async Task<bool> SaleItemsOrder(CreateSaleRequestDto request)
        {
            await _salerepository.AddSaleOrder(request);
            return true;
        }
    }
}
