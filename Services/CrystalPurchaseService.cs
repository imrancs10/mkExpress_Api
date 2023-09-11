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
    public class CrystalPurchaseService : ICrystalPurchaseService
    {
        private readonly ICrystalPurchaseRepository _crystalPurchaseRepository;
        private readonly IMapper _mapper;

        public CrystalPurchaseService(ICrystalPurchaseRepository crystalPurchaseRepository,IMapper mapper)
        {
            _crystalPurchaseRepository = crystalPurchaseRepository;
            _mapper = mapper;
        }

        public async Task<CrystalPurchaseResponse> Add(CrystalPurchaseRequest request)
        {
            if (request == null)
                return default;
            CrystalPurchase crystalPurchase = _mapper.Map<CrystalPurchase>(request);
            if(crystalPurchase.Installments==0)
            {
                crystalPurchase.InstallmentStartDate = DateTime.MinValue;
            }
            if(crystalPurchase.IsWithOutVat)
            {
                crystalPurchase.TotalAmount = crystalPurchase.SubTotalAmount;
                crystalPurchase.Vat = 0;
                crystalPurchase.VatAmount = 0;
                crystalPurchase.CrystalPurchaseDetails.ForEach(res =>
                {
                    res.TotalAmount = res.SubTotalAmount;
                    res.Vat = 0;
                    res.VatAmount = 0;
                });
            }
            return _mapper.Map<CrystalPurchaseResponse>(await _crystalPurchaseRepository.Add(crystalPurchase));
        }

        public async Task<int> Delete(int id)
        {
            return await _crystalPurchaseRepository.Delete(id);
        }

        public async Task<CrystalPurchaseResponse> Get(int id)
        {
            return _mapper.Map<CrystalPurchaseResponse>(await _crystalPurchaseRepository.Get(id));
        }

        public async Task<PagingResponse<CrystalPurchaseResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<CrystalPurchaseResponse>>(await _crystalPurchaseRepository.GetAll(pagingRequest));
        }

        public async Task<int> GetCrystalPurchaseNo()
        {
            return await _crystalPurchaseRepository.GetCrystalPurchaseNo();
        }

        public async Task<PagingResponse<CrystalPurchaseResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<CrystalPurchaseResponse>>(await _crystalPurchaseRepository.Search(searchPagingRequest));
        }

        public async Task<CrystalPurchaseResponse> Update(CrystalPurchaseRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
