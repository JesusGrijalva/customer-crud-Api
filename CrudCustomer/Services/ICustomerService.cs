using CrudCustomer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudCustomer.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(int id);
        Task<Customer> GetByIdAsync(int id);
        Task<List<Customer>> GetAllAsync();

    }
}
