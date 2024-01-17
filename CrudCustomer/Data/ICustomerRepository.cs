using CrudCustomer.Data.Repository;
using CrudCustomer.Models;

namespace CrudCustomer.Data
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
    }
}
