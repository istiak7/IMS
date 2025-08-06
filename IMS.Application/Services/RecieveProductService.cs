using IMS.Application.Dtos.Products;
using IMS.Application.ServiceInterface;
using Inventory_Management_System.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Services
{
    public class RecieveProductService : IRecieveProductService
    {
        private readonly IRecieveProduct _recieveProduct;
        public RecieveProductService(IRecieveProduct recieveProduct)
        {
            _recieveProduct = recieveProduct;
        }
        public async Task<bool> RecieveProducts(RecieveProductDto rDto)
        {
           bool result = await _recieveProduct.RecieveProduct(rDto);
            return result;
           
        }
    }
}
