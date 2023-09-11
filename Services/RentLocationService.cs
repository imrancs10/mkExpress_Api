using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Rents;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Rents;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class RentLocationService : IRentLocationService
    {
        private readonly IRentLocationRepository _rentLocationRepository;
        private readonly IMapper _mapper;
        public RentLocationService(IRentLocationRepository rentLocationRepository, IMapper mapper)
        {
            _mapper = mapper;
            _rentLocationRepository = rentLocationRepository;
        }
        public async Task<RentLocationResponse> Add(RentLocationRequest request)
        {
            RentLocation rentLocation = _mapper.Map<RentLocation>(request);
            return _mapper.Map<RentLocationResponse>(await _rentLocationRepository.Add(rentLocation));
        }

        public async Task<int> Delete(int id)
        {
            return await _rentLocationRepository.Delete(id);
        }

        public async Task<RentLocationResponse> Get(int id)
        {
            return _mapper.Map<RentLocationResponse>(await _rentLocationRepository.Get(id));
        }

        public async Task<PagingResponse<RentLocationResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<RentLocationResponse>>(await _rentLocationRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<RentLocationResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<RentLocationResponse>>(await _rentLocationRepository.Search(searchPagingRequest));
        }

        public async Task<RentLocationResponse> Update(RentLocationRequest request)
        {
            RentLocation rentLocation = _mapper.Map<RentLocation>(request);
            return _mapper.Map<RentLocationResponse>(await _rentLocationRepository.Update(rentLocation));
        }
    }
}
