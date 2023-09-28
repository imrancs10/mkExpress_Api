namespace MKExpress.API.Contants
{
    public enum ShipmentStatusEnum
    {
        Created = 1,
        ReadyForPickup,
        AssignedForPickup,
        PickedUp,
        Received,
        Stored,
        AssignedForDelivery,
        OutForDelivery,
        AssignedForReturn,
        Delivered,
        FailedAttempt,
        OnHold,
        OutForReturn,
        Returned,
        AssignedForTransfer,
        OutForTransfer,
        InTransit,
        Canceled,
        Lost,
        Owned,
        Released
    }
}
