using CrudCustomer.Data;
using CrudCustomer.Models;
using CrudCustomer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllAsync()
        {
            try
            {
                var customers = await _customerService.GetAllAsync();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<Customer>>> GetByIdAsync(int id)
        {
            try
            {
                if (id < 0) { return BadRequest($"Invalid 'Id', must be greater than 0. Current value 'id': {id}"); }

                var customers = await _customerService.GetByIdAsync(id);

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> CreateAsync(Customer customer)
        {
            try
            {
                if (customer == null) { return BadRequest("Cannot insert an empty record"); }
                if (string.IsNullOrWhiteSpace(customer.FirstName) || string.IsNullOrWhiteSpace(customer.LastName) || string.IsNullOrWhiteSpace(customer.Email)) { return BadRequest("Cannot insert an empty record"); }

                var customers = await _customerService.CreateAsync(customer);

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateAsync(Customer customer)
        {
            try
            {
                if (customer == null) { return BadRequest("Cannot update an empty record"); }
                                        
                if (customer.Id <= 0) { return BadRequest($"Invalid 'Id', must be greater than 0. Current value 'id': {customer.Id}"); }

                var customers = await _customerService.UpdateAsync(customer);

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<Customer>>> DeleteByIdAsync(int id)
        {
            try
            {
                if (id < 0) { return BadRequest($"Invalid 'Id', must be greater than 0. Current value 'id': {id}"); }

                var customers = await _customerService.DeleteAsync(id);

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
