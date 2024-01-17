using CrudCustomer.Data;
using CrudCustomer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudCustomer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            return await _customerRepo.InsertAsync(customer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _customerRepo.DeleteAsync(id);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            
            return await _customerRepo.GetAllAsync();

        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _customerRepo.GetByIdAsync(id);
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            return await _customerRepo.UpdateAsync(customer);
        }
    }
}
