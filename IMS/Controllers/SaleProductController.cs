using IMS.Application.ServiceInterface;
using Inventory_Management_System.Dtos.Purchase;
using Inventory_Management_System.Dtos.Sale;
using Inventory_Management_System.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]

    public class SaleProductController : ControllerBase
    {
        private readonly ISaleService _service;
        public SaleProductController(ISaleService service)
        {
            _service = service;
        }

        [HttpPost("Sale-Product/")]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequestDto request)
        {
            try
            {
                await _service.SaleItemsOrder(request);
                return Ok("Sale Sucessfully!");
            }
            catch (DbUpdateException message)
            {
                return BadRequest(message.Message);
            }
            catch (Exception message)
            {
                return BadRequest(message.Message);
            }
        }
    }
}
