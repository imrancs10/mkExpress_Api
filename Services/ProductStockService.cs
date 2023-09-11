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
    public class ProductStockService : IProductStockService
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IMapper _mapper;
        public ProductStockService(IProductStockRepository productStockRepository,IMapper mapper)
        {
            _mapper = mapper;
            _productStockRepository = productStockRepository;
        }
        public async Task<List<OrderCrystalResponse>> GetCrystals()
        {
            return _mapper.Map<List<OrderCrystalResponse>>(await _productStockRepository.GetCrystals());
        }

        public async Task<List<OrderUsedCrystalResponse>> GetOrderUsedCrystals(int OrderDetailId)
        {
            return _mapper.Map<List<OrderUsedCrystalResponse>>(await _productStockRepository.GetOrderUsedCrystals(OrderDetailId));
        }

        public async Task<int> SaveOrderUsedCrystals(List<OrderUsedCrystalRequest> orderUsedCrystals)
        {
            List<OrderUsedCrystal> orderUsedCrystal = _mapper.Map<List<OrderUsedCrystal>>(orderUsedCrystals);
            return await _productStockRepository.SaveOrderUsedCrystals(orderUsedCrystal);
        }
    }
}
