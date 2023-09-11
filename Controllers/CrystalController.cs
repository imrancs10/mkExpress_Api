using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class CrystalController : ControllerBase
    {
        private readonly IMasterCrystalService _crystalService;
        public CrystalController(IMasterCrystalService crystalService)
        {
            _crystalService = crystalService;
        }

        [ProducesResponseType(typeof(MasterCrystalResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.CrystalMasterPath)]
        public async Task<MasterCrystalResponse> AddMasterCrystal([FromBody] MasterCrystalRequest request)
        {
            return await _crystalService.Add(request);
        }

        [ProducesResponseType(typeof(MasterCrystalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalMasterByIdPath)]
        public async Task<MasterCrystalResponse> GetMasterCrystal([FromRoute] int Id)
        {
            return await _crystalService.Get(Id);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterCrystalResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalMasterPath)]
        public async Task<PagingResponse<MasterCrystalResponse>> GetAllMasterCrystal([FromQuery] PagingRequest pagingRequest)
        {
            return await _crystalService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterCrystalResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalMasterSearchPath)]
        public async Task<PagingResponse<MasterCrystalResponse>> SearchMasterCrystal([FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _crystalService.Search(pagingRequest);
        }
        
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalMasterGetCrystalIdPath)]
        public async Task<int> GetNextMasterCrystalId()
        {
            return await _crystalService.GetNextCrystalNo();
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.CrystalMasterDeletePath)]
        public async Task<int> DeleteMasterCrystal([FromRoute] int Id)
        {
            return await _crystalService.Delete(Id);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.CrystalMasterPath)]
        public async Task<MasterCrystalResponse> UpdateMasterCrystal([FromBody] MasterCrystalRequest request)
        {
            return await _crystalService.Update(request);
        }
    }
}
