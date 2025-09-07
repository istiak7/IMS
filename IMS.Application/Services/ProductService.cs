using IMS.Application.Helpers;
using IMS.Application.ServiceInterface;
using IMS.Core.Utility;
using Inventory_Management_System.Dtos.Products;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {

            _productRepository = productRepository;

        }
        public async Task<List<ViewProductDto>> GetProducts()
        {

            return await _productRepository.GetProducts();

        }

        public async Task<ResponseModel> GetByProductId(int id)
        {

            return await _productRepository.GetByProductId(id);

        }

        public async Task<ResponseModel> AddProduct(CreateProductDto product)
        {

            return await _productRepository.AddProduct(product);

        }

        public async Task<ResponseModel> UpdateProduct(int id, CreateProductDto product)
        {

            return await _productRepository.UpdateProduct(id, product);

        }

        public async Task<ResponseModel> DeleteProduct(int id)
        {

            return await _productRepository.DeleteProduct(id);

        }
    }
}
