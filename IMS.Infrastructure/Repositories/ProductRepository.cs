using IMS.Application.Helpers;
using IMS.Core.Utility;
using Inventory_Management_System.ApplicationDb;
using Inventory_Management_System.Dtos.Products;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using System.Text.Json;

namespace IMS.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext Context)
        {
            _context = Context;
        }

        public async Task<List<ViewProductDto>> GetProducts()
        {  

            List<ViewProductDto> ProductDto = [];
            
            var Products = await _context.Products.ToListAsync();
               

            foreach (var product in Products)
             {
                 ViewProductDto dto = new ViewProductDto()
                    {
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        BrandId = product.BrandId,
                        CategoryId = product.CategoryId

                    };
                    ProductDto.Add(dto);
            }
            return ProductDto;

        }

        public async Task<ResponseModel> GetByProductId(int id)
        {
            var Product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
            
            if(Product == null)
            {
                return Utilities.GetNoDataFoundMsg("No Data is Found");
            }
            ViewProductDto dto = new()
            {
                Name = Product.Name,
                Description = Product.Description,
                Price = Product.Price,
                BrandId = Product.BrandId,
                CategoryId = Product.CategoryId
            };
            return Utilities.GetSuccessMsg("ok", dto);
        }

        public async Task<ResponseModel> AddProduct(CreateProductDto product)
        {
            Debug.WriteLine("Debug pointer 1");

            var ExistingProduct = await _context.Products.FirstOrDefaultAsync(name => name.Name == product.Name);
            if (ExistingProduct != null)
            {
                return Utilities.GetNoDataFoundMsg();
            }
            Debug.WriteLine("Debug pointer 2");

            var NewProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                CreatedAt = DateTime.UtcNow
            };
            try
            {
                await _context.Products.AddAsync(NewProduct);
                await _context.SaveChangesAsync();
                Debug.WriteLine("From try block");
                return Utilities.GetSuccessMsg("",NewProduct);
            }
            catch (Exception)
            {
                Debug.WriteLine("Product doesn't added");
                throw new InvalidOperationException("Failed to Add Product");
            }
            
        }

        public async Task<ResponseModel> UpdateProduct(int id, CreateProductDto product)
        {
            var ExistingProduct = await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (ExistingProduct == null)
            {
                return Utilities.GetNoDataFoundMsg();
            }

            ExistingProduct.Name = product.Name;
            ExistingProduct.Description = product.Description;
            ExistingProduct.Price = product.Price;
            ExistingProduct.BrandId = product.BrandId;
            ExistingProduct.CategoryId = product.CategoryId;
            ExistingProduct.UpdatedAt = DateTime.UtcNow;

            try
            {
                _context.Products.Update(ExistingProduct);
                await _context.SaveChangesAsync();
                var dto = new ViewProductDto
                {
                    Name = ExistingProduct.Name,
                    Description = ExistingProduct.Description,
                    Price = ExistingProduct.Price,
                    BrandId = ExistingProduct.BrandId,
                    CategoryId = ExistingProduct.CategoryId
                };
                return Utilities.GetSuccessMsg("", dto);
            }
            
            catch(DbUpdateException)
            {
                throw new DbUpdateException("Failed to Update data");
            }
            catch (Exception)
            {
                throw new Exception("An Error Occurs");
            }
          
        }

        public async Task<ResponseModel> DeleteProduct(int id)
        {
            var ExistingProduct = await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (ExistingProduct == null)
            {
                return Utilities.GetNoDataFoundMsg();
            }
            _context.Products.Remove(ExistingProduct);
            await _context.SaveChangesAsync();
            return Utilities.GetSuccessMsg("Successfully Deleted");
        }
    }
}
