using Bogus;
using CrudCustomer.Data.Repository;
using CrudCustomer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CrudCustomer.Data
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository()
        {
           using (var context = new ApplicationDbContext())
            {
                if(!context.Customers.ToList().Any())
                {
                    var faker = new Faker<Customer>()
                   .RuleFor(c => c.FirstName, f => f.Person.FirstName)
                   .RuleFor(c => c.LastName, f => f.Person.LastName)
                   .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
                   .RuleFor(c => c.Created, f => DateTimeOffset.Now.AddMonths(-f.Random.Int(1, 12)))
                   .RuleFor(c => c.Updated, f => DateTimeOffset.Now.AddDays(-f.Random.Int(1, 7)));

                    var customers = faker.Generate(160);

                    context.Customers.AddRange(customers);

                    context.SaveChanges();
                }
                
            }
        }
    }
}
