﻿using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface ICrudService<TRequest, TResponse> where TRequest : class where TResponse : class
    {
        Task<TResponse> Add(TRequest request);
        Task<TResponse> Update(TRequest request);
        Task<int> Delete(int id);
        Task<TResponse> Get(int id);
        Task<PagingResponse<TResponse>> GetAll(PagingRequest pagingRequest);
        Task<PagingResponse<TResponse>> Search(SearchPagingRequest searchPagingRequest);
    }
}