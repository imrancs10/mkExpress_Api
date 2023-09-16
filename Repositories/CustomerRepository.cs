using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MKExpressDbContext _context;
        public CustomerRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<Customer> Add(Customer customer)
        {
            var oldCustomer = await _context.Customers.Where(x => x.Contact1 == customer.Contact1 && x.Firstname == customer.Firstname).CountAsync();
            if (oldCustomer > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.CustomerAlreadyExistError, StaticValues.CustomerAlreadyExistMessage);
            }
            var entity = _context.Customers.Attach(customer);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int customerId)
        {
            Customer customer = await _context.Customers
                .Where(customer => customer.Id == customerId)
                .FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (customer.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            customer.IsDeleted = true;
            var entity = _context.Customers.Update(customer);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<Customer> Get(int customerId)
        {
            return await _context.Customers.Where(customer => customer.Id == customerId).FirstOrDefaultAsync();
        }

        public async Task<Customer> Update(Customer customer)
        {
            EntityEntry<Customer> oldCustomer = _context.Update(customer);
            oldCustomer.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldCustomer.Entity;
        }

        public async Task<PagingResponse<Customer>> GetAll(PagingRequest pagingRequest)
        {
            var comparer = new DistinctCustomerComparer();
            var data = await _context.Customers
                //.Include(x=>x.Orders.Where(y=>!y.IsCancelled && !y.IsDeleted))
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Firstname)
                .ToListAsync();
            PagingResponse<Customer> pagingResponse = new PagingResponse<Customer>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Distinct(comparer).Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Distinct(comparer).Count()
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Customer>> Search(SearchPagingRequest searchPagingRequest)
        {
            var comparer = new DistinctCustomerComparer();
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.Customers
                .Where(customer => !customer.IsDeleted &&
                        string.IsNullOrEmpty(searchTerm) ||
                        customer.Firstname.Contains(searchTerm) ||
                        customer.Lastname.Contains(searchTerm) ||
                        customer.Branch.Contains(searchTerm) ||
                        customer.TRN.Contains(searchTerm) ||
                        customer.Contact1.Contains(searchTerm) ||
                        customer.Contact2.Contains(searchTerm)
                    )
                .OrderBy(x => x.Firstname)
                    .ToListAsync();
            //GetOrderCountByContactNo
            var filterData = data.Distinct(comparer).Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList();
          
            PagingResponse<Customer> pagingResponse = new PagingResponse<Customer>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = filterData,
                TotalRecords = data.Distinct(comparer).Count()
            };
            return pagingResponse;
        }

        public async Task<List<Customer>> GetCustomers(string contactNo)
        {

            return await _context.Customers
                //.Include(x=>x.Orders.Where(y => !y.IsCancelled && !y.IsDeleted))
               .Where(x => !x.IsDeleted && x.Contact1 == contactNo)
               .OrderBy(x => x.Firstname)
               .ToListAsync();
        }
    }

    public class DistinctCustomerComparer : IEqualityComparer<Customer>
    {
        public bool Equals([AllowNull] Customer x, [AllowNull] Customer y)
        {
            return x.Contact1 == y.Contact1;
        }

        public int GetHashCode([DisallowNull] Customer obj)
        {
            return obj.Contact1.GetHashCode();
        }
    }
}
