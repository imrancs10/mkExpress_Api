using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Data;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class CustomerMeasurementRepository : ICustomerMeasurementRepository
    {
        private readonly MKExpressDbContext _context;
        public CustomerMeasurementRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<CustomerMeasurement> AddUpdateMeasurement(CustomerMeasurement customerMeasurement)
        {
            try
            {
                CustomerMeasurement oldCustomerMeasurement = await _context.CustomerMeasurements
                       .Where(x => x.CustomerId == customerMeasurement.CustomerId && x.MeasurementCustomerName == customerMeasurement.MeasurementCustomerName)
                       .FirstOrDefaultAsync();
                if (oldCustomerMeasurement != null)
                {
                    _context.CustomerMeasurements.Remove(oldCustomerMeasurement);
                    await _context.SaveChangesAsync();
                }
                customerMeasurement.Id = 0;
                EntityEntry<CustomerMeasurement> entity = _context.CustomerMeasurements.Attach(customerMeasurement);
                entity.State = EntityState.Added;
                await _context.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<CustomerMeasurement>> AddUpdateMeasurement(List<CustomerMeasurement> customerMeasurements)
        {
            if (customerMeasurements.Count == 0)
                return new List<CustomerMeasurement>();

            var uniqueMeasurements = customerMeasurements
                .GroupBy(x => x.MeasurementCustomerName)
                .Select(x => x.First())
                .ToList();

            var customerNames = uniqueMeasurements
                .Select(x => x.MeasurementCustomerName)
                .ToList();
            int customerId = uniqueMeasurements.First().CustomerId;

            List<CustomerMeasurement> oldCustomerMeasurement = await _context.CustomerMeasurements
                       .Where(x => x.CustomerId == customerId && customerNames.Contains(x.MeasurementCustomerName))
                       .ToListAsync();

            if (oldCustomerMeasurement.Count > 0)
            {
                _context.CustomerMeasurements.RemoveRange(oldCustomerMeasurement);
                await _context.SaveChangesAsync();
            }

            await _context.CustomerMeasurements.AddRangeAsync(uniqueMeasurements);
            await _context.SaveChangesAsync();
            return uniqueMeasurements;
        }

        public async Task<CustomerMeasurement> GetMeasurement(int customerId)
        {
            return await _context.CustomerMeasurements
                .Where(x => x.CustomerId == customerId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CustomerMeasurement>> GetMeasurements(string contactNo)
        {
            var customerIds = await _context.Customers
                .Where(x => !x.IsDeleted && x.Contact1 == contactNo)
                .Select(x => x.Id)
                .ToListAsync();

            if (customerIds.Count == 0)
                return new List<CustomerMeasurement>();

            return await _context.CustomerMeasurements
                .Where(x => !x.IsDeleted && customerIds.Contains(x.CustomerId))
                .OrderBy(x => x.MeasurementCustomerName)
                .ToListAsync();
        }
        public async Task<int> UpdateMeasurement(List<UpdateMeasurementRequest> updateMeasurementRequest)
        {
            List<int> orderDetailIds = updateMeasurementRequest.Select(x => x.OrderDetailId).ToList();
            List<OrderDetail> orderDetail = await _context.OrderDetails.Where(x => orderDetailIds.Contains(x.Id)).ToListAsync();
            if (orderDetail.Count == 0)
                return default;
            foreach (OrderDetail order in orderDetail)
            {
                var currentRequest = updateMeasurementRequest.Where(x => x.OrderDetailId == order.Id).FirstOrDefault();
                if (currentRequest != null)
                {

                    order.BackDown = currentRequest.BackDown;
                    order.Bottom = currentRequest.Bottom;
                    order.Chest = currentRequest.Chest;
                    order.Deep = currentRequest.Deep;
                    order.Description = currentRequest.Description;
                    order.Extra = currentRequest.Extra;
                    order.Hipps = currentRequest.Hipps;
                    order.Length = currentRequest.Length;
                    order.MeasurementCustomerName = currentRequest.MeasurementCustomerName;
                    order.Neck = currentRequest.Neck;
                    order.Shoulder = currentRequest.Shoulder;
                    order.Size = currentRequest.Size;
                    order.Sleeve = currentRequest.Sleeve;
                    order.SleeveLoose = currentRequest.SleeveLoose;
                    order.Waist = currentRequest.Waist;
                    order.Note = "Update Measurements";
                }
            }
            _context.AttachRange(orderDetail);
            return await _context.SaveChangesAsync();
        }
    }
}
