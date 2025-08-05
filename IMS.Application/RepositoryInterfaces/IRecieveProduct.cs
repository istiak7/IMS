using IMS.Application.Dtos.Products;

namespace Inventory_Management_System.Repositories.Interfaces
{
    public interface IRecieveProduct
    {
        Task<bool> RecieveProduct(RecieveProductDto Rdto);

    }
}
