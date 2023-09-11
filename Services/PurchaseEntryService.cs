using AutoMapper;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class PurchaseEntryService : IPurchaseEntryService
    {
        private readonly IPurchaseEntryRepository _purchaseEntryRepository;
        private readonly IProductStockRepository _productStockRepository;
        private readonly IMapper _mapper;
        public PurchaseEntryService(IPurchaseEntryRepository purchaseEntryRepository, IProductStockRepository productStockRepository, IMapper mapper)
        {
            _mapper = mapper;
            _purchaseEntryRepository = purchaseEntryRepository;
            _productStockRepository = productStockRepository;
        }

        public async Task<PurchaseEntryResponse> Add(PurchaseEntryRequest purchaseEntryRequest)
        {
            PurchaseEntry purchaseEntry = _mapper.Map<PurchaseEntry>(purchaseEntryRequest);
            var result = _mapper.Map<PurchaseEntryResponse>(await _purchaseEntryRepository.Add(purchaseEntry));
            if (result.PurchaseEntryId > 0)
            {
                await AddNewStocks(purchaseEntry.PurchaseEntryDetails);
            }
            return result;
        }

        public async Task<int> Delete(int id)
        {
            var oldPurchaseEntry = await _purchaseEntryRepository.Get(id);
            var stock = _mapper.Map<List<ProductStock>>(oldPurchaseEntry.PurchaseEntryDetails);
            var result = await _purchaseEntryRepository.Delete(id);
            if (result > 0)
            {
                await _productStockRepository.DecreaseStocks(stock);
            }
            return result;
        }

        public async Task<PurchaseEntryResponse> Get(int id)
        {
            return _mapper.Map<PurchaseEntryResponse>(await _purchaseEntryRepository.Get(id));
        }

        public async Task<PagingResponse<PurchaseEntryResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<PurchaseEntryResponse>>(await _purchaseEntryRepository.GetAll(pagingRequest));
        }

        public async Task<int> GetPurchaseNo()
        {
            return await _purchaseEntryRepository.GetPurchaseNo();
        }

        public async Task<PagingResponse<PurchaseEntryResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<PurchaseEntryResponse>>(await _purchaseEntryRepository.Search(searchPagingRequest));
        }

        public async Task<PurchaseEntryResponse> Update(PurchaseEntryRequest purchaseEntryRequest)
        {
            var oldPurchaseEntry = await _purchaseEntryRepository.Get(purchaseEntryRequest.PurchaseEntryId);
            if (oldPurchaseEntry == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            var stock = _mapper.Map<List<ProductStock>>(oldPurchaseEntry.PurchaseEntryDetails);

            PurchaseEntry purchaseEntry = _mapper.Map<PurchaseEntry>(purchaseEntryRequest);
            var result = _mapper.Map<PurchaseEntryResponse>(await _purchaseEntryRepository.Update(purchaseEntry));
            //await AddNewStocks(purchaseEntry.PurchaseEntryDetails);
            if (await _productStockRepository.DecreaseStocks(stock) > 0)
            {
                var updateStock = _mapper.Map<List<ProductStock>>(purchaseEntryRequest.PurchaseEntryDetails);
                await _productStockRepository.IncreaseStocks(updateStock);
            }
            return result;
        }

        private async Task<int> AddNewStocks(List<PurchaseEntryDetail> purchaseEntryDetails)
        {
            var stocks = _mapper.Map<List<ProductStock>>(purchaseEntryDetails);
            stocks.ForEach(res => res.Id = 0);
            return await _productStockRepository.AddNewStocks(stocks);
        }
    }
}
