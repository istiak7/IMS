using IMS.Application.Dtos.Customer;

namespace Inventory_Management_System.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<ViewCustomerDto>> GetCustomers();
        Task<ViewCustomerDto> GetByCustomerId(int id);
        Task<bool> AddCustomer(CreateCustomerDto customer);
        Task<ViewCustomerDto> UpdateCustomer(int id, CreateCustomerDto customer);
        Task<bool> DeleteCustomer(int id);

    }
}
