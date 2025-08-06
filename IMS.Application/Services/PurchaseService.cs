using IMS.Application.ServiceInterface;
using Inventory_Management_System.Dtos.Purchase;
using Inventory_Management_System.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaserepository;
        public PurchaseService(IPurchaseRepository _purchaserepository)
        {
            _purchaserepository = _purchaserepository;
        }
        public async Task<bool> AddPurchase(CreatePurchaseRequestDto request)
        {
            bool result = await _purchaserepository.AddPurchase(request);
            return result;
        }
    }
}
