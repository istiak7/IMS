using Inventory_Management_System.Dtos.Products;
using Inventory_Management_System.Dtos.SaleDto;

namespace Inventory_Management_System.Repositories.Interfaces
{
    public interface ISalesApproval
    {
      //  Task<bool> CanApproveSale(SaleManagerDto salemanagerDto);
        Task<int> GetTotalNormalStockAsync(int productId, int warehouseId);
        Task<int> GetTotalSoldStockAsync(int productId, int warehouseId);
        Task<int> GetCurrentSaleQuantityAsync(int saleDetailId);
        //Task<SaleDetails> GetSaleDetailByIdAsync(int id);
        //Task ApproveSaleDetailAsync(SaleDetail saleDetail);
        //Task AddSaleManagerAsync(SaleManager manager);
        Task<bool> MoveToApprove(int warehouseid, int saledetailsid);
    }
}
