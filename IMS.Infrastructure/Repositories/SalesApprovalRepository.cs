using Inventory_Management_System.ApplicationDb;
using Inventory_Management_System.Dtos.Products;
using Inventory_Management_System.Dtos.SaleDto;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.Repositories
{
    public class NotFoundStockException : Exception
    {
        public NotFoundStockException(string message) : base(message) { }
    }
    public class SalesApprovalRepository : ISalesApproval
    {

        #region DBContext

        private readonly ApplicationDbContext Context;
        public SalesApprovalRepository(ApplicationDbContext Context)
        {
            this.Context = Context;
        }

        #endregion

        #region GET

        public async Task<int> GetTotalNormalStockAsync(int productId, int warehouseId)
        {
            //Total Normal Product Based on Warehouse, Product.
            var TotalNormalProductPerWarehouse = await Context.ProductReport.
                Where(report => report.Stock.PurchaseDetails.ProductId == productId &&
            
            report.Stock.WarehouseId == warehouseId).
            GroupBy(report => report.Stock.PurchaseDetails.Product.Name).
            Select(g => new ViewProductReportDto
            {
                ProductName = g.Key,
                CountNormalProduct = g.Sum(r => r.Normal)
            }).FirstOrDefaultAsync();

            if (TotalNormalProductPerWarehouse == null)
            {
                throw new NotFoundStockException("No Available in Stock");
            }

            return TotalNormalProductPerWarehouse.CountNormalProduct;
        }

        public async Task<int> GetTotalSoldStockAsync(int productId, int warehouseId)
        {
            //Total Sold Product Based on Warehouse, Product
            int totalSoldProductPerWarehouse = await Context.SalesManager
             .Where(m =>
                 m.WarehouseId == warehouseId &&
                 m.SaleDetails.ProductId == productId &&
                 m.SaleDetails.Status == "Approved")
             .SumAsync(m => m.SaleDetails.Quantity);

            return totalSoldProductPerWarehouse;
        }

        public async Task<int> GetCurrentSaleQuantityAsync(int saleDetailId)
        {
            //Curent Quantity Which means Current Order Quantity
            var CurrentQuantity = await Context.SaleDetails.Where(CQ => CQ.Id == saleDetailId).
                Select(s => s.Quantity).FirstOrDefaultAsync();

            return CurrentQuantity;
        }

        #endregion
        public async Task<bool> MoveToApprove(int WarehouseId, int SaleDetailsId)
        {
            var saleDetails = await Context.SaleDetails
             .FirstOrDefaultAsync(s => s.Id == SaleDetailsId);

            if (saleDetails != null)
            {
                saleDetails.Status = "Approved";
                await Context.SaveChangesAsync();
                var Manager = new SaleManager
                {
                    WarehouseId = WarehouseId,
                    SaleDetailsId = SaleDetailsId,
                    CreatedAt = DateTime.UtcNow
                };
                Context.SalesManager.Add(Manager);
                await Context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
    }
}
