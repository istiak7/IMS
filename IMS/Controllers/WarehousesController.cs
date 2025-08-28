using ClosedXML.Excel;
using IMS.Application.Dtos.WarehouseInfo;
using IMS.Application.Helpers;
using IMS.Application.ServiceInterface;
using IMS.Application.Services;
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
        public WarehousesController(IWarehouseService WarehouseService)
        {
            _WarehouseService = WarehouseService;
        }

        // Get All Warehouses
        [HttpGet("Warehouses/")]
        public async Task<IActionResult> GetWarehouses()
        {

            return Ok(await _WarehouseService.GetWarehouses());

        }

        //Get Warehouse By Id
        [HttpGet("Warehouses/{id}")]
        public async Task<IActionResult> GetByWarehouseId(int id)
        {

            return Ok(await _WarehouseService.GetByWarehouseId(id));

        }

        //Add Warehouse
        [HttpPost("/Warehouse")]
        public async Task<IActionResult> AddWarehouse([FromForm] CreateWarehouseDto warehouse)
        {

           return Ok(await _WarehouseService.AddWarehouse(warehouse));
          
        }

        //Update Warehouse
        [HttpPut("/Warehouse/{id}")]
        public async Task<IActionResult> UpdateWarehouse(int id, [FromForm] CreateWarehouseDto warehouse)
        {

            return Ok(await _WarehouseService.UpdateWarehouse(id, warehouse));
      
        }

        //Delete Warehouse
        [HttpDelete("/Warehouse{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {

            return Ok(await _WarehouseService.DeleteWarehouse(id));

        }

        //Download Excel Sheet
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
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }
    }
}
