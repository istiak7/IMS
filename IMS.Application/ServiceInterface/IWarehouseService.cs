using IMS.Application.Dtos.WarehouseInfo;
using IMS.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IWarehouseService
    {
        Task<List<ViewWarehouseDto>> GetWarehouses();
        Task<ResponseModel> GetByWarehouseId(int id);
        Task<ResponseModel> AddWarehouse(CreateWarehouseDto warehouse);
        Task<ResponseModel> UpdateWarehouse(int id, CreateWarehouseDto warehouse);
        Task<ResponseModel> DeleteWarehouse(int id);
        Task<byte[]> DownloadWarehouseInfoExcel();
    }
}
