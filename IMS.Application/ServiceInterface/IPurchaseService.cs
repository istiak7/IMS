using Inventory_Management_System.Dtos.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IPurchaseService
    {
        Task<bool> AddPurchase(CreatePurchaseRequestDto request);
    }
}
