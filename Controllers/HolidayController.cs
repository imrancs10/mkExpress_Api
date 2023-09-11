using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IMasterHolidayService _masterHolidayService;
        private readonly IMasterHolidayNameService _masterHolidayNameService;
        private readonly IMasterHolidayTypeService _masterHolidayTypeService;

        public HolidayController(IMasterHolidayService masterHolidayService, IMasterHolidayNameService masterHolidayNameService, IMasterHolidayTypeService masterHolidayTypeService)
        {
            _masterHolidayService = masterHolidayService;
            _masterHolidayNameService = masterHolidayNameService;
            _masterHolidayTypeService = masterHolidayTypeService;
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidayIsHolidayPath)]
        public async Task<bool> IsHoliday([FromQuery] DateTime holidayDate)
        {
            return await _masterHolidayService.IsHoliday(holidayDate);
        }

        [ProducesResponseType(typeof(MasterHolidayResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidayGetByDatePath)]
        public async Task<MasterHolidayResponse> GetHolidayByDate([FromQuery] DateTime holidayDate)
        {
            return await _masterHolidayService.GetHolidayByDate(holidayDate);
        }

        [ProducesResponseType(typeof(List<MasterHolidayResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidayGetByMonthYearPath)]
        public async Task<List<MasterHolidayResponse>> GetHolidayByMonthYear([FromRoute] int month, [FromRoute] int year)
        {
            return await _masterHolidayService.GetHolidayByMonthYear(month, year);
        }

        [ProducesResponseType(typeof(MasterHolidayResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MasterHolidayResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.HolidayPath)]
        public async Task<MasterHolidayResponse> AddHoliday([FromBody] MasterHolidayRequest masterHoliday)
        {
            return await _masterHolidayService.Add(masterHoliday);
        }

        [ProducesResponseType(typeof(MasterHolidayResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.HolidayPath)]
        public async Task<MasterHolidayResponse> UpdateHoliday([FromBody] MasterHolidayRequest masterHoliday)
        {
            return await _masterHolidayService.Update(masterHoliday);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.HolidayDeletePath)]
        public async Task<int> DeleteHoliday([FromRoute(Name = "id")] int HolidayId)
        {
            return await _masterHolidayService.Delete(HolidayId);
        }

        [ProducesResponseType(typeof(MasterHolidayResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterHolidayResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidayByIdPath)]
        public async Task<MasterHolidayResponse> GetHoliday([FromRoute] int id)
        {
            return await _masterHolidayService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterHolidayResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<MasterHolidayResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidayPath)]
        public async Task<PagingResponse<MasterHolidayResponse>> GetAllHoliday([FromQuery] PagingRequest pagingRequest)
        {
            return await _masterHolidayService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterHolidayResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidaySearchPath)]
        public async Task<PagingResponse<MasterHolidayResponse>> SearchHoliday([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _masterHolidayService.Search(searchPagingRequest);
        }


        [ProducesResponseType(typeof(MasterHolidayNameResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MasterHolidayNameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.HolidayNamePath)]
        public async Task<MasterHolidayNameResponse> AddHolidayName([FromBody] MasterHolidayNameRequest masterHolidayName)
        {
            return await _masterHolidayNameService.Add(masterHolidayName);
        }

        [ProducesResponseType(typeof(MasterHolidayNameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.HolidayNamePath)]
        public async Task<MasterHolidayNameResponse> UpdateHolidayName([FromBody] MasterHolidayNameRequest masterHolidayName)
        {
            return await _masterHolidayNameService.Update(masterHolidayName);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.HolidayNameDeletePath)]
        public async Task<int> DeleteHolidayName([FromRoute(Name = "id")] int HolidayNameId)
        {
            return await _masterHolidayNameService.Delete(HolidayNameId);
        }

        [ProducesResponseType(typeof(MasterHolidayNameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterHolidayNameResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidayNameByIdPath)]
        public async Task<MasterHolidayNameResponse> GetHolidayName([FromRoute] int id)
        {
            return await _masterHolidayNameService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterHolidayNameResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<MasterHolidayNameResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidayNamePath)]
        public async Task<PagingResponse<MasterHolidayNameResponse>> GetAllHolidayName([FromQuery] PagingRequest pagingRequest)
        {
            return await _masterHolidayNameService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterHolidayNameResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidayNameSearchPath)]
        public async Task<PagingResponse<MasterHolidayNameResponse>> SearchHolidayName([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _masterHolidayNameService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.HolidayTypePath)]
        public async Task<MasterDataTypeResponse> AddHolidayType([FromBody] MasterDataTypeRequest masterHolidayType)
        {
            return await _masterHolidayTypeService.Add(masterHolidayType);
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.HolidayTypePath)]
        public async Task<MasterDataTypeResponse> UpdateHolidayType([FromBody] MasterDataTypeRequest masterHolidayType)
        {
            return await _masterHolidayTypeService.Update(masterHolidayType);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.HolidayTypeDeletePath)]
        public async Task<int> DeleteHolidayType([FromRoute(Name = "id")] int HolidayTypeId)
        {
            return await _masterHolidayTypeService.Delete(HolidayTypeId);
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidayTypeByIdPath)]
        public async Task<MasterDataTypeResponse> GetHolidayType([FromRoute] int id)
        {
            return await _masterHolidayTypeService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataTypeResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<MasterDataTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidayTypePath)]
        public async Task<PagingResponse<MasterDataTypeResponse>> GetAllHolidayType([FromQuery] PagingRequest pagingRequest)
        {
            return await _masterHolidayTypeService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.HolidayTypeSearchPath)]
        public async Task<PagingResponse<MasterDataTypeResponse>> SearchHolidayType([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _masterHolidayTypeService.Search(searchPagingRequest);
        }
    }
}