using IMS.Application.ServiceInterface;
using IMS.Application.Services.Cache;
using IMS.Core.Utility;
using Inventory_Management_System.Dtos.Products;
using Inventory_Management_System.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICacheService _cacheService;
        public ProductController(IProductService productService, ICacheService cacheService)
        {
            _productService = productService;
            _cacheService = cacheService;
        }

        #region Get
        
        [HttpGet("Products/")]
        public async Task<IActionResult> GetProducts()
        {
            var cachekey = "GetProducts";

            var response = await _cacheService.GetDataAsync<List<ViewProductDto>> (cachekey);

            if(response == null)
            {
                response = await _productService.GetProducts();

                if (response is not null)
                {
                    await _cacheService.SetDataAsync(cachekey, response, TimeSpan.FromMinutes(10));
                }
               
            }
            return Ok(response);
        }

        [HttpGet("Products/{id}")]
        public async Task<IActionResult> GetByProductId(int id)
        {
            var cacheKey = $"GetProduct_{id}";

            var response = await _cacheService.GetDataAsync<ResponseModel> (cacheKey);

            if(response == null)
            {
                response = await _productService.GetByProductId(id);

                if(response.Data is not null)
                {
                    await _cacheService.SetDataAsync(cacheKey, response, TimeSpan.FromMinutes(10));

                }

            }
            return Ok(response);
        }

        #endregion Get

        [HttpPost("/Product")]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDto product)
        {
            try
            {
                var cacheKey = "GetProducts";

                await _productService.AddProduct(product);

                await _cacheService.RemoveDataAsync(cacheKey);

                return Ok("Successfully Added Product");
            }
            catch(InvalidOperationException message)
            {
                return BadRequest(message.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An Error Occurs");
            }
               
        }

       
        [HttpPut("/Product/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] CreateProductDto product)
        {
            try
            {
                var response = await _productService.UpdateProduct(id, product);

                if (response.Data == null)
                {
                    return NotFound($"Product with ID {id} is not found");
                }

                var cacheKey = $"GetProduct_{id}";

                await _cacheService.SetDataAsync(cacheKey, response, TimeSpan.FromMinutes(10));

                return Ok(response);
            }
           
            catch (DbUpdateException message)
            {
                return BadRequest(message.Message);
            }
            catch (Exception message)
            {
                return StatusCode(500,message.Message);
            }
         
        }

        
        [HttpDelete("/Product{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProduct(id);

            if (response.IsSuccess == false)
            {
                return NotFound($"Product with ID {id} is not found");
            }

            var cacheKey = $"GetProduct_{id}";

            await _cacheService.RemoveDataAsync(cacheKey);

            return Ok($"Product with ID {id} is Delete Successfully");
        }
    }
}
