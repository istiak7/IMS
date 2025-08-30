using IMS.Application.Helpers;
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
        private readonly ApplicationDbContext Context;
        private readonly IDistributedCache _cache;
        public ProductRepository(ApplicationDbContext Context, IDistributedCache cache)
        {
            this.Context = Context;
            _cache = cache;
        }

        public async Task<List<ViewProductDto>> GetProducts()
        {
            var cachekey = "GetAllProduct";

            List<ViewProductDto> ProductDto = [];

            var cacheData = await _cache.GetStringAsync(cachekey);

            if (!string.IsNullOrEmpty(cacheData))
            {
                ProductDto = JsonSerializer.Deserialize<List<ViewProductDto>>(cacheData);
                
            }
            else
            {
                var Products = await Context.Products.ToListAsync();
                if(Products != null)
                {
                    var serializedData = JsonSerializer.Serialize(Products);
                    var cacheOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
                    await _cache.SetStringAsync(cachekey, serializedData, cacheOptions);
                }


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
            }
            return ProductDto;

        }

        public async Task<ViewProductDto> GetByProductId(int id)
        {
            var Product = await Context.Products.FirstOrDefaultAsync(product => product.Id == id);
            ViewProductDto dto = new()
            {
                Name = Product.Name,
                Description = Product.Description,
                Price = Product.Price,
                BrandId = Product.BrandId,
                CategoryId = Product.CategoryId
            };
            return dto;
        }

        public async Task<bool> AddProduct(CreateProductDto product)
        {
            Debug.WriteLine("Debug pointer 1");

            var ExistingProduct = await Context.Products.FirstOrDefaultAsync(name => name.Name == product.Name);
            if (ExistingProduct != null)
            {
                return false;
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
                await Context.Products.AddAsync(NewProduct);
                await Context.SaveChangesAsync();
                Debug.WriteLine("From try block");
                return true;
            }
            catch (Exception)
            {
                Debug.WriteLine("Product doesn't added");
                throw new InvalidOperationException("Failed to Add Product");
            }
            
        }

        public async Task<ViewProductDto> UpdateProduct(int id, CreateProductDto product)
        {
            var ExistingProduct = await Context.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (ExistingProduct == null)
            {
                return null;
            }

            ExistingProduct.Name = product.Name;
            ExistingProduct.Description = product.Description;
            ExistingProduct.Price = product.Price;
            ExistingProduct.BrandId = product.BrandId;
            ExistingProduct.CategoryId = product.CategoryId;
            ExistingProduct.UpdatedAt = DateTime.UtcNow;

            try
            {
                Context.Products.Update(ExistingProduct);
                await Context.SaveChangesAsync();
                return new ViewProductDto
                {
                    Name = ExistingProduct.Name,
                    Description = ExistingProduct.Description,
                    Price = ExistingProduct.Price,
                    BrandId = ExistingProduct.BrandId,
                    CategoryId = ExistingProduct.CategoryId
                };
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

        public async Task<bool> DeleteProduct(int id)
        {
            var ExistingProduct = await Context.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (ExistingProduct == null)
            {
                return false;
            }
            Context.Products.Remove(ExistingProduct);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
