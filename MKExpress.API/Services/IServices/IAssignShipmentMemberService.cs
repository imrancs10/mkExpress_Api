﻿using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IAssignShipmentMemberService
    {
        Task<PagingResponse<AssignShipmentMemberResponse>> GetCourierRunsheet(PagingRequest pagingRequest, Guid memberId);
    }
}
