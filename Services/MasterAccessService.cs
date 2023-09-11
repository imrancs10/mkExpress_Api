using AutoMapper;
using Org.BouncyCastle.Asn1.Ocsp;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.Web.API.Dto.Request.MasterAccess;
using MKExpress.Web.API.Dto.Response.MasterAccess;
using MKExpress.Web.API.Models;
using MKExpress.Web.API.Repositories.Interfaces;
using MKExpress.Web.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Services
{
    public class MasterAccessService : IMasterAccessService
    {
        private readonly IMasterAccessRepository _masterAccessRepository;
        private readonly IMapper _mapper;
        public MasterAccessService(IMasterAccessRepository masterAccessRepository,IMapper mapper)
        {
            _masterAccessRepository = masterAccessRepository;
            _mapper = mapper;
        }

        public async Task<MasterAccessResponse> Add(MasterAccessRequest masterAccessRequest)
        {
            var request = _mapper.Map<MasterAccess>(masterAccessRequest);
            return _mapper.Map<MasterAccessResponse>(await _masterAccessRepository.Add(request));
        }

        public async Task<bool> ChangePassword(MasterAccessChangePasswordRequest masterAccessChangePasswordRequest)
        {
            return await _masterAccessRepository.ChangePassword(masterAccessChangePasswordRequest);
        }

        public async Task<int> Delete(int id)
        {
           return await _masterAccessRepository.Delete(id);
        }

        public async Task<MasterAccessResponse> Get(int id)
        {
            return _mapper.Map<MasterAccessResponse>(await _masterAccessRepository.Get(id));
        }

        public async Task<PagingResponse<MasterAccessResponse>> GetAll(PagingRequest pagingRequest)
        {
            var data= _mapper.Map<PagingResponse<MasterAccessResponse>>(await _masterAccessRepository.GetAll(pagingRequest));
            return data;
        }

        public async Task<List<MenuResponse>> GetMenus()
        {
            return _mapper.Map<List<MenuResponse>>(await _masterAccessRepository.GetMenus());
        }

        public async Task<bool> IsUsernameExist(string username)
        {
           return await _masterAccessRepository.IsUsernameExist(username);
        }

        public async Task<MasterAccessResponse> MasterAccessLogin(MasterAccessLoginRequest accessLoginRequest)
        {
            return _mapper.Map<MasterAccessResponse>( await _masterAccessRepository.MasterAccessLogin(accessLoginRequest));
        }

        public async Task<PagingResponse<MasterAccessResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterAccessResponse>>(await _masterAccessRepository.Search(searchPagingRequest));
        }

        public async Task<MasterAccessResponse> Update(MasterAccessRequest masterAccessRequest)
        {
            var request = _mapper.Map<MasterAccess>(masterAccessRequest);
            return _mapper.Map<MasterAccessResponse>(await _masterAccessRepository.Update(request));
        }
    }
}
