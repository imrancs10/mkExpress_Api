using Microsoft.EntityFrameworkCore;
using MKExpress.API.Data;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class HealthRepository : IHealthRepository
    {
        private readonly MKExpressDbContext _context;
        public HealthRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public string GetDatabaseName()
        {
            return _context.Database.GetDbConnection().Database;
        }
    }
}
