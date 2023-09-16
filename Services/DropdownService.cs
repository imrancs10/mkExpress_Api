using AutoMapper;
using MKExpress.API.Dto.Response;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class DropdownService : IDropdownService
    {
        private readonly IDropdownRepository _dropdownRepository;
        private readonly IMapper _mapper;
        public DropdownService(IDropdownRepository dropdownRepository, IMapper mapper)
        {
            _dropdownRepository = dropdownRepository;
            _mapper = mapper;
        }


        public async Task<List<DropdownResponse>> Customers(string searchTerm)
        {
            return _mapper.Map<List<DropdownResponse>>(await _dropdownRepository.Customers(searchTerm));
        }

        public async Task<List<DropdownResponse>> JobTitle()
        {
            var result = await _dropdownRepository.JobTitle();
            return _mapper.Map<List<DropdownResponse>>(result);
        }
    }
}
