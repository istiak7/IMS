using Dapper;
using IMS.Infrastructure.Dapper;
using Inventory_Management_System.ApplicationDb;
using Inventory_Management_System.Dtos.Products;
using Inventory_Management_System.Dtos.WarehouseInfo;
using Inventory_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.Repositories
{
    public class InventoryReportRepository : IInventoryReport
    {
        private readonly ApplicationDbContext Context;
        private readonly IDapper _dapper;
        public InventoryReportRepository(ApplicationDbContext Context, IDapper dapper)
        {
            this.Context = Context;
            _dapper = dapper;
        }

        public async Task<List<ViewWarehouseInfoDto>> GetWarehouseInfoById(int id)
        {
            var result = await Context.Stocks.Where(s => s.WarehouseId == id)
                .Include(s => s.PurchaseDetails)
                     .ThenInclude(pd => pd.Product)
                
                .Include(pd => pd.PurchaseDetails.Purchase)
                        .ThenInclude(p => p.Supplier)
                .Select(s => new ViewWarehouseInfoDto
                {
                    PurchaseId = s.PurchaseDetails.Purchase.Id,
                    Supplier = s.PurchaseDetails.Purchase.Supplier.Name,
                    Product = s.PurchaseDetails.Product.Name,
                    Quantity = s.Quantity,
                    PurchaseDate = s.PurchaseDetails.Purchase.CreatedAt
                }).ToListAsync();

            return result;
        }

        public async Task<ViewProductReportDto> GetCountNormalProductById(int id)
        {
            //var result = await Context.ProductReport.Where(report => report.Stock.PurchaseDetails.ProductId == id).
            //   GroupBy(report => report.Stock.PurchaseDetails.Product.Name).
            //   Select(s => new ViewProductReportDto
            //   {
            //       ProductName = s.Key,
            //       CountNormalProduct = s.Sum(r => r.Normal)
            //   }).FirstOrDefaultAsync();

            var sql = @"
                        SELECT ""P"".""Name"" AS ""ProductName"", SUM(""PR"".""Normal"") AS ""CountNormalProduct""

                        FROM ""ProductReport"" AS ""PR""
                        INNER JOIN ""Stocks"" AS ""S"" ON ""PR"".""StockId"" = ""S"".""Id""
                        INNER JOIN ""PurchaseDetails"" AS ""PD"" ON ""PD"".""Id"" = ""S"".""PurchaseDetailsId""
                        INNER JOIN ""Products"" AS ""P"" ON ""PD"".""ProductId"" = ""P"".""Id""

                        WHERE ""PD"".""ProductId"" = @Id 
                        GROUP BY ""P"".""Name""
                        LIMIT 1;
                        ";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            var result = await _dapper.GetByIdAsync<ViewProductReportDto>(sql, parameters);
            System.Diagnostics.Debug.WriteLine(result);
            return result;
        }
    }
}
