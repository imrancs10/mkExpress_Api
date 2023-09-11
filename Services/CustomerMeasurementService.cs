using AutoMapper;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class CustomerMeasurementService : ICustomerMeasurementService
    {
        private readonly ICustomerMeasurementRepository _customerMeasurementRepository;
        private readonly IMapper _mapper;
        public CustomerMeasurementService(ICustomerMeasurementRepository customerMeasurementRepository, IMapper mapper)
        {
            _customerMeasurementRepository = customerMeasurementRepository;
            _mapper = mapper;
        }

        public async Task<CustomerMeasurement> AddUpdateMeasurement(CustomerMeasurement customerMeasurement)
        {
            return await _customerMeasurementRepository.AddUpdateMeasurement(customerMeasurement);
        }

        public async Task<CustomerMeasurementResponse> GetMeasurement(int customerId)
        {
            return _mapper.Map<CustomerMeasurementResponse>(await _customerMeasurementRepository.GetMeasurement(customerId));
        }

        public async Task<List<CustomerMeasurementResponse>> GetMeasurements(string contactNo)
        {
            return _mapper.Map<List<CustomerMeasurementResponse>>(await _customerMeasurementRepository.GetMeasurements(contactNo));
        }


        public async Task<int> UpdateMeasurement(List<UpdateMeasurementRequest> updateMeasurementRequest)
        {
            var result = await _customerMeasurementRepository.UpdateMeasurement(updateMeasurementRequest);
            if (result > 0)
            {
                foreach (var item in updateMeasurementRequest)
                {
                    var measurement = _mapper.Map<CustomerMeasurement>(item);
                    await _customerMeasurementRepository.AddUpdateMeasurement(measurement);
                }
            }
            return result;
        }
    }
}
