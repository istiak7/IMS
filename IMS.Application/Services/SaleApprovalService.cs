using IMS.Application.Interface;
using Inventory_Management_System.Dtos.SaleDto;
using Inventory_Management_System.Repositories.Interfaces;

namespace IMS.Application.Services
{
    public class NotFoundStockException : Exception
    {
        public NotFoundStockException(string message) : base(message) { }
    }
    public class SaleApprovalService : ISaleApprovalService
    {
        private readonly ISalesApproval _salesApprovalRepository;
      
        public SaleApprovalService(ISalesApproval salesApprovalRepository)
        {
            _salesApprovalRepository = salesApprovalRepository;
           
        }
        public async Task<bool> CanApproveSale(SaleManagerDto salemanagerDto)
        {
            int productId = salemanagerDto.ProductId;
            int warehouseId = salemanagerDto.WarehouseId;
            int saleDetailsId = salemanagerDto.SaleDetailsId;

            int TotalNormalStockPerWarehouse = await _salesApprovalRepository.GetTotalNormalStockAsync(productId, warehouseId);

            if (TotalNormalStockPerWarehouse == null)
            {
                throw new NotFoundStockException("No Available in Stock");
            }

            int totalSoldProduct = await _salesApprovalRepository.GetTotalSoldStockAsync(productId, warehouseId);

            int CuurentSaleQuantity = await _salesApprovalRepository.GetCurrentSaleQuantityAsync(saleDetailsId);

           // Total Available Stock Based on this Warehouse, Product
            var AvailableProduct = TotalNormalStockPerWarehouse - totalSoldProduct;
            //Logic if Product is Available in Stock, Move to approve 
            if (AvailableProduct >= CuurentSaleQuantity)
            {
                bool Approve = await _salesApprovalRepository.MoveToApprove(warehouseId, saleDetailsId);
                return Approve;
            }
            return false;
        }
    }
}
