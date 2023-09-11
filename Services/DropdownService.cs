using AutoMapper;
using MKExpress.API.Dto.Response;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class DropdownService : IDropdownService
    {
        private readonly IDropdownRepository _dropdownRepository;
        private readonly IMapper _mapper;
        public DropdownService(IDropdownRepository dropdownRepository, IMapper mapper)
        {
            _dropdownRepository = dropdownRepository;
            _mapper = mapper;
        }

        public Task<List<DropdownResponse>> CustomerOrders()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<DropdownResponse>> Customers(string searchTerm)
        {
            return _mapper.Map<List<DropdownResponse>>(await _dropdownRepository.Customers(searchTerm));
        }

        public async Task<List<DropdownResponse<EmployeeResponse>>> Employee(string searchTerm)
        {
            return _mapper.Map<List<DropdownResponse<EmployeeResponse>>>(await _dropdownRepository.Employee(searchTerm));
        }

        public async Task<List<DropdownResponse>> JobTitle()
        {
            var result = await _dropdownRepository.JobTitle();
            return _mapper.Map<List<DropdownResponse>>(result);
        }

        public async Task<List<DropdownResponse>> Products()
        {
            var result = await _dropdownRepository.Products();
            return _mapper.Map<List<DropdownResponse>>(result);
        }

        public async Task<List<DropdownResponse<SupplierResponse>>> Suppliers()
        {
            var result = await _dropdownRepository.Suppliers();
            return _mapper.Map<List<DropdownResponse<SupplierResponse>>>(result);
        }
        public async Task<List<DropdownResponse>> DesignCategory()
        {
            return _mapper.Map<List<DropdownResponse>>(await _dropdownRepository.DesignCategory());
        }

        public async Task<List<DropdownResponse>> OrderDetailNos(bool excludeDelivered)
        {
            return _mapper.Map<List<DropdownResponse>>(await _dropdownRepository.OrderDetailNos(excludeDelivered));
        }

        public async Task<List<DropdownResponse>> WorkTypes()
        {
            return _mapper.Map<List<DropdownResponse>>(await _dropdownRepository.WorkTypes());
        }
    }
}
