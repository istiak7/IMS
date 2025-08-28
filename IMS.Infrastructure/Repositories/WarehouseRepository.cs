using Dapper;
using IMS.Application.Dtos.WarehouseInfo;
using IMS.Application.Helpers;
using IMS.Core.Utility;
using IMS.Infrastructure.Dapper;
using Inventory_Management_System.ApplicationDb;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace IMS.Infrastructure.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly ApplicationDbContext Context;
        private readonly IDapper _dapper;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30);
        public WarehouseRepository(ApplicationDbContext Context, IDapper dapper, IMemoryCache cache)
        {
            this.Context = Context;
            _dapper = dapper;
            _cache = cache;
        }

        public async Task<List<ViewWarehouseDto>> GetWarehouses()
        {
            var cachekey = "Warehouses";
            if (!_cache.TryGetValue(cachekey, out List<ViewWarehouseDto>? warehouselist))
            {
                var sql = "SELECT * FROM \"Warehouses\"";

                var Warehouses = await _dapper.GetAllAsync<ViewWarehouseDto>(sql);

                warehouselist = [];

                foreach (var warehouse in Warehouses)
                {
                    warehouselist.Add(warehouse);
                }

                _cache.Set(cachekey, warehouselist, _cacheExpiration);
            }
            return warehouselist;
            //var sql = "SELECT * FROM \"Warehouses\"";

            //var Warehouses = await _dapper.GetAllAsync<ViewWarehouseDto>(sql);

            //List<ViewWarehouseDto> WarehouseDto = [];

            //foreach (var warehouse in Warehouses)
            //{
            //    WarehouseDto.Add(warehouse);
            //}

            //return WarehouseDto;
        }

        public async Task<ResponseModel> GetByWarehouseId(int id)
        {
            var cachekey = $"warehouse_{id}";

            if(!_cache.TryGetValue(cachekey, out ViewWarehouseDto ? warehouse))
            {
                var sql = $"SELECT * FROM \"Warehouses\" WHERE \"Id\" = @Id";

                var parameters = new DynamicParameters();

                parameters.Add("Id", id);

                warehouse = await _dapper.GetByIdAsync<ViewWarehouseDto>(sql, parameters);

                _cache.Set(cachekey, warehouse, _cacheExpiration);
            }
            
            //var sql = $"SELECT * FROM \"Warehouses\" WHERE \"Id\" = @Id";

            //var parameters = new DynamicParameters();

            //parameters.Add("Id", id);

            //var Warehouse = await _dapper.GetByIdAsync<ViewWarehouseDto>(sql, parameters);


            
            if(warehouse != null)
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
                return Utilities.GetSuccessMsg("Successfully Updated");
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
