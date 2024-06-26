﻿using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IAssignShipmentMemberRepository
    {
        Task<PagingResponse<AssignShipmentMember>> GetCourierRunsheet(PagingRequest pagingRequest,Guid memberId);
    }
}
