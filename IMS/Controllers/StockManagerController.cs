using IMS.Application.Dtos.Products;
using IMS.Application.ServiceInterface;
using Inventory_Management_System.Dtos.StockInsertDto;
using Inventory_Management_System.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class StockManagerController : ControllerBase
    {
        private readonly IStockService _stockservice;
        private readonly IRecieveProductService _RecieveProductService;
        public StockManagerController(IStockService stockservice, IRecieveProductService _RecieveProductService)
        {
            _stockservice = stockservice;
            _RecieveProductService = _RecieveProductService;
        }
        [HttpPost("Add-from-Purchase/")]
        public async Task<IActionResult> AddStockFromPurchase([FromForm] StockInsertDto stockDto)
        {
            var result = await _stockservice.AddStockFromPurchase(stockDto);
            if(result == false)
            {
                return NotFound();
            }
            return Ok("Successfully stocked from Purchase!");
        }



        [HttpPost("Receive")]
        public async Task<IActionResult> ReceiveProduct([FromBody] RecieveProductDto dto)
        {
            var result = await  _RecieveProductService.RecieveProducts(dto);
            if (!result)
                return BadRequest("Invalid data or stock not found");
            return Ok("Product received and recorded");
        }
    }
}
