using IMS.Application.ServiceInterface;
using Inventory_Management_System.Dtos.Purchase;
using Inventory_Management_System.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class PurchaseProductController : ControllerBase
    {
        private readonly IPurchaseService _service;
        public PurchaseProductController(IPurchaseService service)
        {
            _service = service;
        }

        [HttpPost("Purchase-Product/")]
        public async Task<IActionResult> CreatePurchase([FromBody] CreatePurchaseRequestDto request)
        {
            try
            {
                await _service.AddPurchase(request);
                return Ok("Successfully Purchase Added.");
            }
            catch (DbUpdateException)
            {
                return BadRequest("Invalid Supplier Id or ProductId");
            }
            catch
            {
                return StatusCode(500,"UnExpected Error");
            }
            
            
        }
    }
}
