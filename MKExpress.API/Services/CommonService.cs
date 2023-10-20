using MKExpress.API.Contants;
using MKExpress.API.Exceptions;
using MKExpress.API.Logger;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Services
{
    public class CommonService : ICommonService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoggerManager _loggerManager;
        public CommonService(IHttpContextAccessor httpContextAccessor,ILoggerManager loggerManager)
        {
            _httpContextAccessor =httpContextAccessor;
            _loggerManager = loggerManager;
        }
        public Guid GetLoggedInUserId()
        {
            if (!(bool)_httpContextAccessor.HttpContext?.Request?.Headers?.ContainsKey(StaticValues.ConstValue_UserId))
            {
                _loggerManager.LogWarn($"{StaticValues.Message_UserIdHeaderNotPresentInRequest}, Class : ContainerRepository, Method : GetLoggedInUserId");
                throw new BusinessRuleViolationException(StaticValues.Error_UserIdHeaderNotPresentInRequest, StaticValues.Message_UserIdHeaderNotPresentInRequest);
            }

            string? value = _httpContextAccessor.HttpContext?.Request.Headers[StaticValues.ConstValue_UserId].ToString();
            if (string.IsNullOrEmpty(value))
            {
                _loggerManager.LogWarn($"{StaticValues.Message_UserIdNotPresentInHeader}, Class : ContainerRepository, Method : GetLoggedInUserId");
                throw new BusinessRuleViolationException(StaticValues.Error_UserIdNotPresentInHeader, StaticValues.Message_UserIdNotPresentInHeader);
                
            }

            if (!Guid.TryParse(value, out Guid newUserId))
            {
                _loggerManager.LogWarn($"Invalid logged-in userId {newUserId}, Class : ContainerRepository, Method : GetLoggedInUserId");
                throw new BusinessRuleViolationException(StaticValues.Error_UserIdInvalidPresentInHeader, StaticValues.Message_UserIdInvalidPresentInHeader);
            }
            return newUserId;
        }

        public bool ValidateThirdPartyShipmentStatus(ShipmentStatusEnum shipmentStatus)
        {
            return shipmentStatus switch
            {
                ShipmentStatusEnum.Created or ShipmentStatusEnum.Stored => true,
                _ => false,
            };
        }
    }
}
