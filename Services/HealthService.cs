using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class HealthService : IHealthService
    {
        private readonly IHealthRepository _healthRepository;
        public HealthService(IHealthRepository healthRepository)
        {
            _healthRepository = healthRepository;
        }
        public string GetDatabaseName()
        {
            return _healthRepository.GetDatabaseName();
        }
    }
}
