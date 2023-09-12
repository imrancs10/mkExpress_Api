using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Expense;
using MKExpress.API.Dto.Request.Orders;
using MKExpress.API.Dto.Request.Rents;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Customer;
using MKExpress.API.Dto.Response.Expense;
using MKExpress.API.Dto.Response.Orders;
using MKExpress.API.Dto.Response.Rents;
using MKExpress.API.Dto.Response.Report;
using MKExpress.API.Models;
using MKExpress.Web.API.Dto.Request;
using MKExpress.Web.API.Dto.Request.MasterAccess;
using MKExpress.Web.API.Dto.Response.MasterAccess;
using MKExpress.Web.API.Models;
using System.Linq;

namespace MKExpress.API.Config
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
        }
    }
}