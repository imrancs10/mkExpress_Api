using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly IMapper _mapper;
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        public ExpenseTypeService(IExpenseTypeRepository expenseTypeRepository, IMapper mapper)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _mapper = mapper;
        }
        public async Task<MasterDataTypeResponse> Add(MasterDataTypeRequest request)
        {
            ExpenseType expenseType = _mapper.Map<ExpenseType>(request);
            return _mapper.Map<MasterDataTypeResponse>(await _expenseTypeRepository.Add(expenseType));
        }

        public async Task<int> Delete(int id)
        {
            return await _expenseTypeRepository.Delete(id);
        }

        public async Task<MasterDataTypeResponse> Get(int id)
        {
            return _mapper.Map<MasterDataTypeResponse>(await _expenseTypeRepository.Get(id));
        }

        public async Task<PagingResponse<MasterDataTypeResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterDataTypeResponse>>(await _expenseTypeRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<MasterDataTypeResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterDataTypeResponse>>(await _expenseTypeRepository.Search(searchPagingRequest));
        }

        public async Task<MasterDataTypeResponse> Update(MasterDataTypeRequest request)
        {
            ExpenseType expenseType = _mapper.Map<ExpenseType>(request);
            return _mapper.Map<MasterDataTypeResponse>(await _expenseTypeRepository.Update(expenseType));
        }
    }
}
