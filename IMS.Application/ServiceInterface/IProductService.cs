using IMS.Core.Utility;
using Inventory_Management_System.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IProductService
    {
        Task<List<ViewProductDto>> GetProducts();
        Task<ResponseModel> GetByProductId(int id);
        Task<ResponseModel> AddProduct(CreateProductDto product);
        Task<ResponseModel> UpdateProduct(int id, CreateProductDto product);
        Task<ResponseModel> DeleteProduct(int id);
    }
}
