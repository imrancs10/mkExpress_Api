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
    public class RentDetailService : IRentDetailService
    {
        private readonly IRentDetailRepository _rentDetailRepository;
        private readonly IMapper _mapper;
        public RentDetailService(IRentDetailRepository rentDetailRepository, IMapper mapper)
        {
            _mapper = mapper;
            _rentDetailRepository = rentDetailRepository;
        }
        public async Task<RentDetailResponse> Add(RentDetailRequest request)
        {
            RentDetail rentDetail = _mapper.Map<RentDetail>(request);
            return _mapper.Map<RentDetailResponse>(await _rentDetailRepository.Add(rentDetail));
        }

        public async Task<int> Delete(int id)
        {
            return await _rentDetailRepository.Delete(id);
        }

        public async Task<RentDetailResponse> Get(int id)
        {
            return _mapper.Map<RentDetailResponse>(await _rentDetailRepository.Get(id));
        }

        public async Task<PagingResponse<RentDetailResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<RentDetailResponse>>(await _rentDetailRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<RentTransactionResponse>> GetDueRents(bool isPaid, PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<RentTransactionResponse>>(await _rentDetailRepository.GetDueRents(isPaid, pagingRequest));
        }

        public async Task<List<RentTransactionResponse>> GetRentTransations(int rentDetailId = 0)
        {
            return _mapper.Map<List<RentTransactionResponse>>(await _rentDetailRepository.GetRentTransations(rentDetailId));
        }

        public async Task<int> PayDeuRents(RentPayRequest rentPayRequest, int userId)
        {
            return await _rentDetailRepository.PayDeuRents(rentPayRequest, userId);
        }

        public async Task<PagingResponse<RentDetailResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<RentDetailResponse>>(await _rentDetailRepository.Search(searchPagingRequest));
        }

        public async Task<PagingResponse<RentTransactionResponse>> SearchDeuRents(bool isPaid, SearchPagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<RentTransactionResponse>>(await _rentDetailRepository.SearchDeuRents(isPaid, pagingRequest));
        }

        public async Task<RentDetailResponse> Update(RentDetailRequest request)
        {
            RentDetail rentDetail = _mapper.Map<RentDetail>(request);
            return _mapper.Map<RentDetailResponse>(await _rentDetailRepository.Add(rentDetail));
        }
    }
}
