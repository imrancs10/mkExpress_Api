using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using Org.BouncyCastle.Ocsp;

namespace MKExpress.API.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _repository;
        private readonly IMapper _mapper;
        public UserRoleService(IUserRoleRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<UserRoleResponse> AddRoleAsync(UserRoleRequest req)
        {
            var userRole = _mapper.Map<UserRole>(req);
            return _mapper.Map< UserRoleResponse>( await _repository.AddRoleAsync(userRole));
        }

        public async Task<PagingResponse<UserRoleResponse>> GetAllRolesAsync(PagingRequest request)
        {
            return _mapper.Map<PagingResponse<UserRoleResponse>>(await _repository.GetAllRolesAsync(request));
        }

        public async Task<UserRoleResponse> GetRoleByIdAsync(int id)
        {
           return _mapper.Map<UserRoleResponse>(await _repository.GetRoleByIdAsync(id));
        }

        public async Task<PagingResponse<UserRoleResponse>> SearchRolesAsync(SearchPagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<UserRoleResponse>>(await _repository.SearchRolesAsync(pagingRequest));
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            return await _repository.DeleteRoleAsync(id);
        }

        public async Task<bool> UpdateRoleAsync(UserRoleRequest req)
        {
            var userRole = _mapper.Map<UserRole>(req);
            return await _repository.UpdateRoleAsync(userRole);
        }
    }
}
