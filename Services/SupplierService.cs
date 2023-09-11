using AutoMapper;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }
        public async Task<SupplierResponse> Add(SupplierRequest supplierRequest)
        {
            Supplier supplier = _mapper.Map<Supplier>(supplierRequest);
            return _mapper.Map<SupplierResponse>(await _supplierRepository.Add(supplier));
        }

        public async Task<int> Delete(int supplierId)
        {
            return await _supplierRepository.Delete(supplierId);
        }

        public async Task<SupplierResponse> Get(int supplierId)
        {
            return _mapper.Map<SupplierResponse>(await _supplierRepository.Get(supplierId));
        }

        public async Task<PagingResponse<SupplierResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<SupplierResponse>>(await _supplierRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<SupplierResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<SupplierResponse>>(await _supplierRepository.Search(searchPagingRequest));
        }

        public async Task<SupplierResponse> Update(SupplierRequest supplierRequest)
        {
            var oldSupplier = await _supplierRepository.Get(supplierRequest.Id);
            if (oldSupplier != null && oldSupplier.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }

            Supplier supplier = _mapper.Map<Supplier>(supplierRequest);
            return _mapper.Map<SupplierResponse>(await _supplierRepository.Update(supplier));
        }
    }
}
