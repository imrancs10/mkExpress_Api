using Microsoft.EntityFrameworkCore;
using MKExpress.API.Data;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class EmployeeEMIPaymentRepository : IEmployeeEMIPaymentRepository
    {
        private readonly MKExpressDbContext _context;
        public EmployeeEMIPaymentRepository(MKExpressDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeEMIPayment>> Add(List<EmployeeEMIPayment> employeeEMIPayments)
        {
            _context
               .EmployeeEMIPayments
               .AttachRange(employeeEMIPayments);
            await _context.SaveChangesAsync();
            return employeeEMIPayments;
        }

        public async Task<int> DeleteByAdvancePaymentId(int advancePaymentId)
        {
            var emiPayments = await _context.EmployeeEMIPayments.Where(x => x.AdvancePaymentId == advancePaymentId).ToListAsync();
            if (emiPayments.Count == 0)
                return default;

            _context.EmployeeEMIPayments.RemoveRange(emiPayments);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeeEMIPayment>> GetByEmployeeId(int employeeId)
        {
            return await _context.EmployeeEMIPayments
                .Include(x => x.EmployeeAdvancePayment)
                .Where(x => x.EmployeeAdvancePayment.EmployeeId == employeeId)
                .OrderBy(x => x.DeductionYear).ThenBy(x => x.DeductionMonth)
                .ToListAsync();
        }
    }
}
