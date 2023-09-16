using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class DropdownController : ControllerBase
    {
        private readonly IDropdownService _dropdownService;
        public DropdownController(IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }

        [ProducesResponseType(typeof(List<DropdownResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DropdownJobTitlePathPath)]
        public async Task<List<DropdownResponse>> GetJobTitleDropdown()
        {
            return await _dropdownService.JobTitle();
        }
    }
}
