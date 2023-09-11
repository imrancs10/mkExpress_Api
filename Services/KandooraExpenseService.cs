using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class KandooraExpenseService : IKandooraExpenseService
    {
        private readonly IKandooraExpenseRepository _kandoorExpenseRepository;
        private readonly IMapper _mapper;
        public KandooraExpenseService(IKandooraExpenseRepository kandoorExpenseRepository, IMapper mapper)
        {
            _kandoorExpenseRepository = kandoorExpenseRepository;
            _mapper = mapper;
        }
        public async Task<EachKandooraExpenseHeadResponse> AddExpenseHead(EachKandooraExpenseHeadRequest request)
        {
            EachKandooraExpenseHead eachKandooraExpenseHead = _mapper.Map<EachKandooraExpenseHead>(request);
            return _mapper.Map<EachKandooraExpenseHeadResponse>(await _kandoorExpenseRepository.AddExpenseHead(eachKandooraExpenseHead));
        }

        public async Task<List<EachKandooraExpenseResponse>> AddExpense(List<EachKandooraExpenseRequest> request)
        {
            List<EachKandooraExpense> eachKandooraExpense = _mapper.Map<List<EachKandooraExpense>>(request);
            return _mapper.Map<List<EachKandooraExpenseResponse>>(await _kandoorExpenseRepository.AddExpense(eachKandooraExpense));
        }

        public async Task<int> DeleteExpenseHead(int id)
        {
            return await _kandoorExpenseRepository.DeleteExpenseHead(id);
        }

        public async Task<EachKandooraExpenseHeadResponse> GetExpenseHead(int id)
        {
            return _mapper.Map<EachKandooraExpenseHeadResponse>(await _kandoorExpenseRepository.GetExpenseHead(id));
        }

        public async Task<PagingResponse<EachKandooraExpenseHeadResponse>> GetAllExpenseHead(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<EachKandooraExpenseHeadResponse>>(await _kandoorExpenseRepository.GetAllExpenseHead(pagingRequest));
        }

        public async Task<PagingResponse<EachKandooraExpenseHeadResponse>> SearchExpenseHead(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<EachKandooraExpenseHeadResponse>>(await _kandoorExpenseRepository.SearchExpenseHead(searchPagingRequest));
        }

        public async Task<EachKandooraExpenseHeadResponse> UpdateExpenseHead(EachKandooraExpenseHeadRequest request)
        {
            EachKandooraExpenseHead eachKandooraExpenseHead = _mapper.Map<EachKandooraExpenseHead>(request);
            return _mapper.Map<EachKandooraExpenseHeadResponse>(await _kandoorExpenseRepository.UpdateExpenseHead(eachKandooraExpenseHead));
        }

        public async Task<PagingResponse<EachKandooraExpenseResponse>> GetAllExpense(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<EachKandooraExpenseResponse>>(await _kandoorExpenseRepository.GetAllExpense(pagingRequest));
        }

        public async Task<decimal> GetTotalOfExpense()
        {
            return await _kandoorExpenseRepository.GetTotalOfExpense();
        }
    }
}
