#nullable enable
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class DropdownRepository : IDropdownRepository
    {
        private readonly MKExpressDbContext _context;
        public DropdownRepository(MKExpressDbContext context)
        {
            _context = context;
        }

        public Task<List<Dropdown>> CustomerOrders()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Dropdown>> Customers(string? searchTerm)
        {
            searchTerm = searchTerm?.ToLower() ?? string.Empty;
            return await _context.Customers.Where(customer =>!customer.IsDeleted && (
            searchTerm.Equals(string.Empty) ||
            customer.Firstname.Contains(searchTerm) ||
            customer.Lastname.Contains(searchTerm) ||
            customer.Contact1.Contains(searchTerm) ||
            customer.Contact2.Contains(searchTerm) ||
            customer.POBox.Contains(searchTerm) ||
            customer.Branch.Contains(searchTerm)))
                .OrderBy(x => x.Firstname)
                .Select(customer => new Dropdown() { Id = customer.Id, Value = $"{customer.Firstname} {customer.Lastname} {customer.Contact1} {customer.Contact2}", Remark = string.Empty })
                .ToListAsync();
        }

        public async Task<List<Dropdown<Employee>>> Employee(string? searchTerm)
        {
            searchTerm = searchTerm?.ToLower() ?? string.Empty;
            return await _context.Employees
                .Include(x => x.MasterJobTitle)
                .Where(emp => emp.IsActive && !emp.IsDeleted &&
            (searchTerm.Equals(string.Empty) ||
            emp.FirstName.Contains(searchTerm) ||
            emp.LastName.Contains(searchTerm) ||
            emp.Address.Contains(searchTerm) ||
            emp.Contact.Contains(searchTerm) ||
            emp.MasterJobTitle.Code.Contains(searchTerm) ||
            emp.MasterJobTitle.Value.Contains(searchTerm)))
                .OrderBy(x => x.FirstName)
                .Select(emp => new Dropdown<Employee>()
                {
                    Id = emp.Id,
                    Value = $"{emp.FirstName} {emp.LastName}",
                    Remark = string.Empty,
                    Data = emp
                })
                .ToListAsync();
        }

        public async Task<List<Dropdown>> JobTitle()
        {
            return await _context.MasterJobTitles
                .Where(x=>!x.IsDeleted)
               .Select(x =>
                       new Dropdown()
                       {
                           Id = x.Id,
                           Value = x.Value,
                           Code=x.Code
                       })
               .OrderBy(x => x.Value)
               .ToListAsync();
        }

        public async Task<List<Dropdown>> Products()
        {
            var products = await _context.Products.Where(pro=>!pro.IsDeleted).ToListAsync();
            return products.Select(pro =>
                       new Dropdown()
                       {
                           Id = pro.Id,
                           Value = $"{pro.ProductName}"
                       })
               .OrderBy(x => x.Value)
               .ToList();
        }

        public async Task<List<Dropdown<Supplier>>> Suppliers()
        {
            return await _context.Suppliers.Where(x=>!x.IsDeleted)
               .Select(supp => new Dropdown<Supplier>()
               {
                   Id = supp.Id,
                   Value = supp.CompanyName,
                   Remark = string.Empty,
                   Data = supp
               })
               .OrderBy(x => x.Value)
               .ToListAsync();
        }

        public async Task<List<Dropdown>> DesignCategory()
        {
            return await _context.MasterDesignCategories
                .Where(x=>!x.IsDeleted)
              .Select(mdc =>
                      new Dropdown()
                      {
                          Id = mdc.Id,
                          Value = mdc.Value,
                          Code = mdc.Code
                      })
              .OrderBy(x => x.Value)
              .ToListAsync();
        }

        public async Task<List<Dropdown>> OrderDetailNos(bool excludeDelivered)
        {
            return await _context.OrderDetails
                .Where(x => !x.IsDeleted && !x.IsCancelled && (!excludeDelivered || x.Status!=OrderStatusEnum.Delivered.ToString()))
              .Select(mdc =>
                      new Dropdown()
                      {
                          Id = mdc.Id,
                          Value = mdc.OrderNo,
                          Code = mdc.OrderId.ToString(),
                          Remark=mdc.Status,
                          ParentId=mdc.OrderId
                      })
              .OrderByDescending(x => x.Value)
              .ToListAsync();
        }

        public async Task<List<Dropdown>> WorkTypes()
        {
            return await _context.MasterDatas
               .Where(x => !x.IsDeleted && x.MasterDataType=="work_type")
             .Select(mdc =>
                     new Dropdown()
                     {
                         Id = mdc.Id,
                         Value = mdc.Value,
                         Code = mdc.Code,
                         Remark=string.Empty
                     })
             .OrderBy(x => x.Code)
             .ToListAsync();
        }
    }
}
