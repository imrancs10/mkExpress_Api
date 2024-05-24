using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response.User;
using MKExpress.API.Models;
using MKExpress.API.Repository;

namespace MKExpress.API.Services
{
    public class UserRoleMenuMapperService : IUserRoleMenuMapperService
    {
        private readonly IUserRoleMenuMapperRepository _repository;
        private readonly IMapper _mapper;
        public UserRoleMenuMapperService(IUserRoleMenuMapperRepository _repository,IMapper mapper)
        {
            _repository = _repository;
            _mapper = mapper;
        }

        public Task<bool> Add(List<UserRoleMenuMapperRequest> req)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid RoleId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRoleMenuMapperResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRoleMenuMapperResponse>> GetByRoleId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRoleMenuMapperResponse>> SearchRolesAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
