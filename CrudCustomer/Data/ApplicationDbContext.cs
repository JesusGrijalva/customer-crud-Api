using CrudCustomer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace CrudCustomer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDatabase");
        }

    }
}
