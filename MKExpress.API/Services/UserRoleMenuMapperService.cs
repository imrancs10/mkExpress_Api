using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response.User;
using MKExpress.API.Models;
using MKExpress.API.Repository;
using Org.BouncyCastle.Ocsp;

namespace MKExpress.API.Services
{
    public class UserRoleMenuMapperService : IUserRoleMenuMapperService
    {
        private readonly IUserRoleMenuMapperRepository _repository;
        private readonly IMapper _mapper;
        public UserRoleMenuMapperService(IUserRoleMenuMapperRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Add(List<UserRoleMenuMapperRequest> req)
        {
            var userMenu=_mapper.Map<List<UserRoleMenuMapper>>(req);
            return await _repository.Add(userMenu);
        }

        public async Task<bool> DeleteByRoleId(Guid roleId)
        {
            return await _repository.DeleteByRoleId(roleId);
        }

        public async Task<bool> DeleteById(Guid id)
        {
            return await _repository.DeleteById(id);
        }

        public async Task<List<UserRoleMenuMapperResponse>> GetAll()
        {
            return _mapper.Map<List<UserRoleMenuMapperResponse>>(await _repository.GetAll());
        }

        public async Task<List<UserRoleMenuMapperResponse>> GetByRoleId(Guid id)
        {
            return _mapper.Map<List<UserRoleMenuMapperResponse>>(await _repository.GetByRoleId(id));
        }

        public async Task<List<UserRoleMenuMapperResponse>> SearchRolesAsync(string searchTerm)
        {
            return _mapper.Map<List<UserRoleMenuMapperResponse>>(await _repository.SearchRolesAsync(searchTerm));
        }
    }
}
