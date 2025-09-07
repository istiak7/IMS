using ClosedXML.Excel;
using IMS.Application.Dtos.WarehouseInfo;
using IMS.Application.Helpers;
using IMS.Application.ServiceInterface;
using IMS.Application.Services;
using IMS.Core.Utility;
using Inventory_Management_System.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    //[Authorize]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _WarehouseService;
        private readonly ICacheService _cacheService;
        public WarehousesController(IWarehouseService WarehouseService, ICacheService cacheService)
        {
            _WarehouseService = WarehouseService;
            _cacheService = cacheService;
        }

        #region Get

        [HttpGet("Warehouses/")]
        public async Task<IActionResult> GetWarehouses()
        {
            var cacheKey = "GetAllWarehouse";

            var response = await _cacheService.GetDataAsync<List<ViewWarehouseDto>>(cacheKey);

            if(response == null)
            {
                response = await _WarehouseService.GetWarehouses();

                await _cacheService.SetDataAsync(cacheKey, response, TimeSpan.FromMinutes(10));
            }
            return Ok(response);

        }

       
        [HttpGet("Warehouses/{id}")]
        public async Task<IActionResult> GetByWarehouseId(int id)
        {
            var cacheKey = $"GetWarehouse_{id}";

            var response = await _cacheService.GetDataAsync<ResponseModel>(cacheKey);

            if(response == null)
            {
                response = await _WarehouseService.GetByWarehouseId(id);
                await _cacheService.SetDataAsync(cacheKey, response, TimeSpan.FromMinutes(10));
            }

                return Ok(response);
        }

        [HttpGet("/download-ExcelSheet")]
        public async Task<IActionResult> DownloadExcel()
        {

            try
            {
                var ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                var Filename = "WarehouseInfo";

                var Content = await _WarehouseService.DownloadWarehouseInfoExcel();

                return File(Content, ContentType, Filename);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        #endregion Get

        [HttpPost("/Warehouse")]
        public async Task<IActionResult> AddWarehouse([FromForm] CreateWarehouseDto warehouse)
        {
            await _cacheService.RemoveDataAsync("GetAllWarehouse");

            return Ok(await _WarehouseService.AddWarehouse(warehouse));
          
        }

      
        [HttpPut("/Warehouse/{id}")]
        public async Task<IActionResult> UpdateWarehouse(int id, [FromForm] CreateWarehouseDto warehouse)
        {
            var cacheKey = $"GetWarehouse_{id}";

            var response = await _WarehouseService.UpdateWarehouse(id, warehouse);

            await _cacheService.SetDataAsync(cacheKey,response, TimeSpan.FromMinutes(10));

            return Ok(response);
      
        }

      
        [HttpDelete("/Warehouse{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            var cacheKey = $"GetWarehouse_{id}";

            await _cacheService.RemoveDataAsync(cacheKey);

            return Ok(await _WarehouseService.DeleteWarehouse(id));

        }
    }
}
