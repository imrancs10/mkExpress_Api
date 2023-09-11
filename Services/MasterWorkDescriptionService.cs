using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class MasterWorkDescriptionService : IMasterWorkDescriptionService
    {
        private readonly IMasterWorkDescriptionRepository _workDescriptionRepository;
        private readonly IMapper _mapper;
        public MasterWorkDescriptionService(IMasterWorkDescriptionRepository workDescriptionRepository,IMapper mapper)
        {
            _workDescriptionRepository = workDescriptionRepository;
            _mapper = mapper;
        }
        public async Task<MasterDataTypeResponse> Add(MasterDataTypeRequest request)
        {
            MasterWorkDescription masterWorkDescription = _mapper.Map<MasterWorkDescription>(request);
           return _mapper.Map<MasterDataTypeResponse>(await _workDescriptionRepository.Add(masterWorkDescription));
        }

        public async Task<int> Delete(int id)
        {
            return await _workDescriptionRepository.Delete(id);
        }

        public async Task<MasterDataTypeResponse> Get(int id)
        {
            return _mapper.Map<MasterDataTypeResponse>(await _workDescriptionRepository.Get(id));
        }

        public async Task<PagingResponse<MasterDataTypeResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterDataTypeResponse>>(await _workDescriptionRepository.GetAll(pagingRequest));
        }

        public async Task<List<MasterDataTypeResponse>> GetByWorkTypes(string worktype)
        {
            return _mapper.Map<List<MasterDataTypeResponse>>(await _workDescriptionRepository.GetByWorkTypes(worktype));
        }

        public async Task<int> SaveOrderWorkDescription(List<OrderWorkDescriptionRequest> request)
        {
            List<OrderWorkDescription> OrderWorkDescriptions = _mapper.Map<List<OrderWorkDescription>>(request);
            return await _workDescriptionRepository.SaveOrderWorkDescription(OrderWorkDescriptions);
        }

        public async Task<List<OrderWorkDescriptionResponse>> GetOrderWorkDescription(int orderDetailId)
        {
            return _mapper.Map<List<OrderWorkDescriptionResponse>>(await _workDescriptionRepository.GetOrderWorkDescription(orderDetailId));
        }

        public async Task<PagingResponse<MasterDataTypeResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterDataTypeResponse>>(await _workDescriptionRepository.Search(searchPagingRequest));
        }

        public async Task<MasterDataTypeResponse> Update(MasterDataTypeRequest request)
        {
            MasterWorkDescription masterWorkDescription = _mapper.Map<MasterWorkDescription>(request);
            return _mapper.Map<MasterDataTypeResponse>(await _workDescriptionRepository.Update(masterWorkDescription));
        }
    }
}
