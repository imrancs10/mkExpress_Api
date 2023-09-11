using AutoMapper;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class EmployeeAdvancePaymentService : IEmployeeAdvancePaymentService
    {
        private readonly IEmployeeAdvancePaymentRepository _employeeAdvancePaymentRepository;
        private readonly IEmployeeEMIPaymentRepository _employeeEMIPaymentRepository;
        private readonly IMapper _mapper;
        public EmployeeAdvancePaymentService(IEmployeeAdvancePaymentRepository employeeAdvancePaymentRepository, 
            IMapper mapper, 
            IEmployeeEMIPaymentRepository employeeEMIPaymentRepository)
        {
            _employeeAdvancePaymentRepository = employeeAdvancePaymentRepository;
            _mapper = mapper;
            _employeeEMIPaymentRepository = employeeEMIPaymentRepository;
        }
        public async Task<EmployeeAdvancePaymentResponse> Add(EmployeeAdvancePaymentRequest request)
        {
            EmployeeAdvancePayment employeeAdvancePayment = _mapper.Map<EmployeeAdvancePayment>(request);
            return _mapper.Map<EmployeeAdvancePaymentResponse>(await _employeeAdvancePaymentRepository.Add(employeeAdvancePayment));

        }

        public async Task<int> Delete(int id)
        {
            return await _employeeAdvancePaymentRepository.Delete(id);
        }

        public async Task<EmployeeAdvancePaymentResponse> Get(int id)
        {
            return _mapper.Map<EmployeeAdvancePaymentResponse>(await _employeeAdvancePaymentRepository.Get(id));
        }

        public async Task<PagingResponse<EmployeeAdvancePaymentResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<EmployeeAdvancePaymentResponse>>(await _employeeAdvancePaymentRepository.GetAll(pagingRequest));
        }

        public async Task<List<EmployeeEMIPaymentResponse>> GetByEmployeeId(int employeeId)
        {
            return _mapper.Map<List<EmployeeEMIPaymentResponse>>(await _employeeEMIPaymentRepository.GetByEmployeeId(employeeId));
        }

        public async Task<List<EmployeeAdvancePaymentResponse>> GetStatement(int empId)
        {
            return _mapper.Map<List<EmployeeAdvancePaymentResponse>>(await _employeeAdvancePaymentRepository.GetStatement(empId));
        }

        public async Task<PagingResponse<EmployeeAdvancePaymentResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<EmployeeAdvancePaymentResponse>>(await _employeeAdvancePaymentRepository.Search(searchPagingRequest));
        }

        public async Task<EmployeeAdvancePaymentResponse> Update(EmployeeAdvancePaymentRequest request)
        {
            EmployeeAdvancePayment employeeAdvancePayment = _mapper.Map<EmployeeAdvancePayment>(request);
            return _mapper.Map<EmployeeAdvancePaymentResponse>(await _employeeAdvancePaymentRepository.Update(employeeAdvancePayment));
        }
    }
}
