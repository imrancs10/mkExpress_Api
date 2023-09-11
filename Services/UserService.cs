using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> CreateUser(UserRegistrationRequest customerRegistrationRequest)
        {
            var user = _mapper.Map<User>(customerRegistrationRequest);
            return _mapper.Map<UserResponse>(await _userRepository.CreateUser(user));
        }

        public Task<UserResponse> UpdateUser(UpdateUserRequest updateCustomerRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse> GetUser(string email = null)
        {
            return _mapper.Map<UserResponse>(await _userRepository.GetUser(email));
        }

        public async Task<List<UserResponse>> GetUsers()
        {
            return _mapper.Map<List<UserResponse>>(await _userRepository.GetUsers());
        }

        public async Task<int> DeleteUser(int empId)
        {
            return await _userRepository.DeleteUser(empId);
        }
    }
}