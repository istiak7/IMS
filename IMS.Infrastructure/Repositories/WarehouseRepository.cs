using Dapper;
using IMS.Application.Dtos.WarehouseInfo;
using IMS.Application.Helpers;
using IMS.Core.Utility;
using IMS.Infrastructure.Dapper;
using Inventory_Management_System.ApplicationDb;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Security.AccessControl;
using System.Text.Json;

namespace IMS.Infrastructure.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly ApplicationDbContext Context;
        private readonly IDapper _dapper;
        private readonly IDistributedCache _cache;
        public WarehouseRepository(ApplicationDbContext Context, IDapper dapper, IDistributedCache cache)
        {
            this.Context = Context;
            _dapper = dapper;
            _cache = cache;
        }

        public async Task<List<ViewWarehouseDto>> GetWarehouses()
        {
          
            List<ViewWarehouseDto> warehouseList = [];
           
            var sql = "SELECT * FROM \"Warehouses\"";

            var Warehouses = await _dapper.GetAllAsync<ViewWarehouseDto>(sql);

            foreach (var warehouse in Warehouses)
            {
               warehouseList.Add(warehouse);
            }
     
            return warehouseList;

        }

        public async Task<ResponseModel> GetByWarehouseId(int id)
        {

           var sql = $"SELECT * FROM \"Warehouses\" WHERE \"Id\" = @Id";

           var parameters = new DynamicParameters();

           parameters.Add("Id", id);

           var warehouse = await _dapper.GetByIdAsync<ViewWarehouseDto>(sql, parameters);

            if (warehouse != null)
            {
                return Utilities.GetSuccessMsg("Successfully Data Found", warehouse);
            }

            else
            {
                return Utilities.GetNoDataFoundMsg();
            }
        }

        public async Task<ResponseModel> AddWarehouse(CreateWarehouseDto warehouse)
        {
            var ExistingWarehouse = await Context.Warehouses.FirstOrDefaultAsync(name => name.Name == warehouse.Name);

            if (ExistingWarehouse != null)
            {
                return Utilities.GetAlreadyExistMsg("The Data You Already Added");
            }

            var NewWarehouse = new Warehouse
            {
                Name = warehouse.Name,
                Location = warehouse.Location,
                PhoneNumber = warehouse.Phone,
                CreatedAt = DateTime.UtcNow
            };

            await Context.Warehouses.AddAsync(NewWarehouse);

            int affectedRows = await Context.SaveChangesAsync();

            if(affectedRows > 0)
            {
                return Utilities.GetSuccessMsg("Successfully Added New Warehouse");
            }

            else
            {
                return Utilities.GetInternalServerErrorMsg("An Error Occurs");
            }
        }

        public async Task<ResponseModel> UpdateWarehouse(int id, CreateWarehouseDto warehouse)
        {

            var ExistingWarehouse = await Context.Warehouses.FirstOrDefaultAsync(i => i.Id == id);

            if (ExistingWarehouse == null)
            {
                return Utilities.GetNoDataFoundMsg();
            }

            ExistingWarehouse.Name = warehouse.Name;
            ExistingWarehouse.Location = warehouse.Location;
            ExistingWarehouse.PhoneNumber = warehouse.Phone;
            ExistingWarehouse.UpdatedAt = DateTime.UtcNow;

            Context.Warehouses.Update(ExistingWarehouse);
            int affectedRows = await Context.SaveChangesAsync();

            if(affectedRows > 0)
            {

                return Utilities.GetSuccessMsg("Successfully Updated", ExistingWarehouse);
            }

            else
            {
                return Utilities.GetInternalServerErrorMsg("An Error Occurs");
            }
        }

        public async Task<ResponseModel> DeleteWarehouse(int id)
        {
            var ExistingWarehouse = await Context.Warehouses.FirstOrDefaultAsync(i => i.Id == id);

            if (ExistingWarehouse == null)
            {
                return Utilities.GetNoDataFoundMsg();
            }

            Context.Warehouses.Remove(ExistingWarehouse);

            int affectedRow = await Context.SaveChangesAsync();

            if(affectedRow > 0)
            {
                
                return Utilities.GetSuccessMsg("Successfully Deleted", ExistingWarehouse);
            }

            else
            {
                return Utilities.GetInternalServerErrorMsg("An Error Occurs");
            }
        }
    }
}
