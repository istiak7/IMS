using IMS.Application.Dtos.WarehouseInfo;
using IMS.Application.Helpers;
using IMS.Application.ServiceInterface;
using IMS.Core.Utility;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouserepository;
        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            _warehouserepository = warehouseRepository;
        }
        public async Task<List<ViewWarehouseDto>> GetWarehouses()
        {

            return await (_warehouserepository.GetWarehouses());

        }

        public async Task<ResponseModel> GetByWarehouseId(int id)
        {

            return await (_warehouserepository.GetByWarehouseId(id));

        }

        public async Task<ResponseModel> AddWarehouse(CreateWarehouseDto warehouse)
        {

            return await (_warehouserepository.AddWarehouse(warehouse));

        }

        public async Task<ResponseModel> UpdateWarehouse(int id, CreateWarehouseDto warehouse)
        {

            return await (_warehouserepository.UpdateWarehouse(id, warehouse));
           
        }

        public async Task<ResponseModel> DeleteWarehouse(int id)
        {

            return await (_warehouserepository.DeleteWarehouse(id));

        }
    }
}
