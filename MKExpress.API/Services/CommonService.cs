using MKExpress.API.Contants;
using MKExpress.API.Exceptions;
using MKExpress.API.Extension;
using MKExpress.API.Logger;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Services
{
    public class CommonService : ICommonService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoggerManager _loggerManager;
        public CommonService(IHttpContextAccessor httpContextAccessor, ILoggerManager loggerManager)
        {
            _httpContextAccessor = httpContextAccessor;
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

        public string ValidateShipmentStatus(ShipmentStatusEnum currentStatus, ShipmentStatusEnum newStatus)
        {
            return currentStatus switch
            {
                ShipmentStatusEnum.Created => newStatus switch
                {
                    ShipmentStatusEnum.ReadyForPickup => ShipmentStatusEnum.ReadyForPickup.ToFormatString(),
                    ShipmentStatusEnum.AssignedForPickup => ShipmentStatusEnum.AssignedForPickup.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    ShipmentStatusEnum.Canceled => ShipmentStatusEnum.Canceled.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.ReadyForPickup => newStatus switch
                {
                    ShipmentStatusEnum.Canceled => ShipmentStatusEnum.Canceled.ToFormatString(),
                    ShipmentStatusEnum.PickedUp => ShipmentStatusEnum.PickedUp.ToFormatString(),
                    ShipmentStatusEnum.PickupRescheduled => ShipmentStatusEnum.PickupRescheduled.ToFormatString(),
                    ShipmentStatusEnum.PickupFailed => ShipmentStatusEnum.PickupFailed.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.AssignedForPickup => newStatus switch
                {
                    ShipmentStatusEnum.ReadyForPickup => ShipmentStatusEnum.ReadyForPickup.ToFormatString(),
                    ShipmentStatusEnum.Canceled => ShipmentStatusEnum.Canceled.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.PickupRescheduled => newStatus switch
                {
                    ShipmentStatusEnum.Received => ShipmentStatusEnum.Received.ToFormatString(),
                    ShipmentStatusEnum.PickupRescheduled => ShipmentStatusEnum.PickupRescheduled.ToFormatString(),
                    ShipmentStatusEnum.PickedUp => ShipmentStatusEnum.PickedUp.ToFormatString(),
                    ShipmentStatusEnum.Canceled => ShipmentStatusEnum.Canceled.ToFormatString(),
                    ShipmentStatusEnum.PickupFailed => ShipmentStatusEnum.PickupFailed.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.PickupFailed => newStatus switch
                {
                    ShipmentStatusEnum.Received => ShipmentStatusEnum.Received.ToFormatString(),
                    ShipmentStatusEnum.PickupRescheduled => ShipmentStatusEnum.PickupRescheduled.ToFormatString(),
                    ShipmentStatusEnum.PickedUp => ShipmentStatusEnum.PickedUp.ToFormatString(),
                    ShipmentStatusEnum.Canceled => ShipmentStatusEnum.Canceled.ToFormatString(),
                    ShipmentStatusEnum.PickupFailed => ShipmentStatusEnum.PickupFailed.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.PickedUp => newStatus switch
                {
                    ShipmentStatusEnum.Received => ShipmentStatusEnum.Received.ToFormatString(),
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.Received => newStatus switch
                {
                    ShipmentStatusEnum.Stored => ShipmentStatusEnum.Stored.ToFormatString(),
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.AssignedForDelivery => ShipmentStatusEnum.AssignedForDelivery.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.Stored => newStatus switch
                {
                    ShipmentStatusEnum.AssignedForTransfer => ShipmentStatusEnum.AssignedForTransfer.ToFormatString(),
                    ShipmentStatusEnum.ContainerSealed => ShipmentStatusEnum.ContainerSealed.ToFormatString(),
                    ShipmentStatusEnum.OnHold => ShipmentStatusEnum.OnHold.ToFormatString(),
                    ShipmentStatusEnum.Stored => ShipmentStatusEnum.Stored.ToFormatString(),
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.AssignedForReturn => ShipmentStatusEnum.AssignedForReturn.ToFormatString(),
                    ShipmentStatusEnum.AssignedForDelivery => ShipmentStatusEnum.AssignedForDelivery.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.AssignedForTransfer => newStatus switch
                {
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.OnHold => newStatus switch
                {
                    ShipmentStatusEnum.Stored => ShipmentStatusEnum.Stored.ToFormatString(),
                    ShipmentStatusEnum.ConsigneeInfoUpdated => ShipmentStatusEnum.ConsigneeInfoUpdated.ToFormatString(),
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.ConsigneeInfoUpdated => newStatus switch
                {
                    ShipmentStatusEnum.MarkAsResolved => ShipmentStatusEnum.MarkAsResolved.ToFormatString(),
                    ShipmentStatusEnum.AssignedForReturn => ShipmentStatusEnum.AssignedForReturn.ToFormatString(),
                    ShipmentStatusEnum.AssignedForDelivery => ShipmentStatusEnum.AssignedForDelivery.ToFormatString(),
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.AssignedForDelivery => newStatus switch
                {
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.OutForDelivery => ShipmentStatusEnum.OutForDelivery.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.OutForDelivery => newStatus switch
                {
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.CalledConsignee => ShipmentStatusEnum.CalledConsignee.ToFormatString(),
                    ShipmentStatusEnum.Delivered => ShipmentStatusEnum.Delivered.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.CalledConsignee => newStatus switch
                {
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.CalledConsignee => ShipmentStatusEnum.CalledConsignee.ToFormatString(),
                    ShipmentStatusEnum.FailedDelivery => ShipmentStatusEnum.FailedDelivery.ToFormatString(),
                    ShipmentStatusEnum.Delivered => ShipmentStatusEnum.Delivered.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.FailedDelivery => newStatus switch
                {
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.Received => ShipmentStatusEnum.Received.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.AssignedForReturn => newStatus switch
                {
                    ShipmentStatusEnum.OutForReturn => ShipmentStatusEnum.OutForReturn.ToFormatString(),
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.ContainerSealed => newStatus switch
                {
                    ShipmentStatusEnum.InTransit => ShipmentStatusEnum.InTransit.ToFormatString(),
                    ShipmentStatusEnum.ReceivedInTransit => ShipmentStatusEnum.ReceivedInTransit.ToFormatString(),
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.ReceivedInTransit => newStatus switch
                {
                    ShipmentStatusEnum.InTransit => ShipmentStatusEnum.InTransit.ToFormatString(),
                    ShipmentStatusEnum.ReceivedInTransit => ShipmentStatusEnum.ReceivedInTransit.ToFormatString(),
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.InTransit => newStatus switch
                {
                    ShipmentStatusEnum.InTransit => ShipmentStatusEnum.InTransit.ToFormatString(),
                    ShipmentStatusEnum.ReceivedInTransit => ShipmentStatusEnum.ReceivedInTransit.ToFormatString(),
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.OutForReturn => newStatus switch
                {
                    ShipmentStatusEnum.Returned => ShipmentStatusEnum.Returned.ToFormatString(),
                    ShipmentStatusEnum.ReturnedToCustomer => ShipmentStatusEnum.ReturnedToCustomer.ToFormatString(),
                    ShipmentStatusEnum.Received => ShipmentStatusEnum.Received.ToFormatString(),
                    ShipmentStatusEnum.Lost => ShipmentStatusEnum.Lost.ToFormatString(),
                    ShipmentStatusEnum.MarkedAsLost => ShipmentStatusEnum.MarkedAsLost.ToFormatString(),
                    ShipmentStatusEnum.ChangedManually => ShipmentStatusEnum.ChangedManually.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.Canceled => newStatus switch
                {
                    ShipmentStatusEnum.Canceled => ShipmentStatusEnum.Canceled.ToFormatString(),
                    _ => ThrowException()
                },
                ShipmentStatusEnum.Lost => newStatus switch
                {
                    _ => ThrowException()
                },
                ShipmentStatusEnum.Delivered => newStatus switch
                {
                    _ => ThrowException()
                },
                ShipmentStatusEnum.Returned => newStatus switch
                {
                    _ => ThrowException()
                },
                ShipmentStatusEnum.ReturnedToCustomer => newStatus switch
                {
                    _ => ThrowException()
                },
                ShipmentStatusEnum.MarkedAsLost => newStatus switch
                {
                    _ => ThrowException()
                },
                ShipmentStatusEnum.ChangedManually => newStatus switch
                {
                    _ => newStatus.ToFormatString()
                },
                ShipmentStatusEnum.NoStatus => newStatus switch
                {
                    _ => ShipmentStatusEnum.Created.ToFormatString()
                },
                _ => newStatus.ToFormatString(),
            };
            ;
        }

        public string ValidateShipmentStatus(string currentStatus, ShipmentStatusEnum newStatus)
        {
            var isValid = Enum.TryParse(currentStatus, out ShipmentStatusEnum result);
            if (!isValid)
                throw new BusinessRuleViolationException(StaticValues.Error_InvalidCurrentShipmentStatus, StaticValues.Message_InvalidCurrentShipmentStatus);
            return ValidateShipmentStatus(result, newStatus);
        }

        public bool ValidateThirdPartyShipmentStatus(ShipmentStatusEnum shipmentStatus)
        {
            return shipmentStatus switch
            {
                ShipmentStatusEnum.Created or ShipmentStatusEnum.Stored => true,
                _ => false,
            };
        }

        private string ThrowException()
        {
            throw new BusinessRuleViolationException(StaticValues.Error_InvalidShipmentStatusAssign, StaticValues.Message_InvalidShipmentStatusAssign);
        }
    }
}
