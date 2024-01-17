using Bogus;
using CrudCustomer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudCustomer.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public async Task<bool> DeleteAsync(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var customer = await context.Set<T>().Where(_ => _.Id == id).FirstOrDefaultAsync();

                if (customer != null)
                {
                    context.Set<T>().Remove(customer);
                    context.SaveChanges();
                }

                return true;
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var list = await context.Set<T>().ToListAsync();

                return list;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var customer = await context.Set<T>().Where(_ => _.Id == id).FirstOrDefaultAsync();

                return customer;
            }
        }

        public async Task<T> InsertAsync(T record)
        {
            using (var context = new ApplicationDbContext())
            {
                record.Created = DateTimeOffset.Now;
                record.Updated = DateTimeOffset.Now;

                var inserted = await context.Set<T>().AddAsync(record);

                context.SaveChanges();

                return inserted.Entity;
            }

        }

        public async Task<T> UpdateAsync(T record)
        {
            using (var context = new ApplicationDbContext())
            {
                var existingRecord = await context.Set<T>().FindAsync(record.Id);

                if (existingRecord != null)
                {
                    context.Entry(existingRecord).CurrentValues.SetValues(record);

                    existingRecord.Updated = DateTimeOffset.Now;

                    await context.SaveChangesAsync();
                }

                return existingRecord;
            }
        }
    }
}
