using CrudCustomer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudCustomer.Data.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
       Task<List<T>> GetAllAsync();
       Task<T> GetByIdAsync(int id);
       Task<T> InsertAsync(T record);
       Task<T> UpdateAsync(T record);
       Task<bool> DeleteAsync(int id);
    }
}
