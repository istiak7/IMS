using Inventory_Management_System.Dtos.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface ISaleService
    {
        Task<bool> SaleItemsOrder(CreateSaleRequestDto request);
    }
}
