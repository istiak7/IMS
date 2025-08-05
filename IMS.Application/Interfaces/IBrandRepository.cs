using IMS.Application.Dtos.Brand;

namespace Inventory_Management_System.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Task<List<ViewBrandDto>> GetBrands();
        Task<ViewBrandDto> GetByBrandId(int id);
        Task<bool> AddBrand(CreateBrandDto brand);
        Task<ViewBrandDto> UpdateBrand(int id, CreateBrandDto brand);
        Task<bool> DeleteBrand(int id);
    }
}
