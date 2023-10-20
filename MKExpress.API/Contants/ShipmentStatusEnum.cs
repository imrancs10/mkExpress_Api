namespace MKExpress.API.Contants
{
    public enum ShipmentStatusEnum
    {
        Created = 1,
        ReadyForPickup,
        AssignedForPickup,
        PickedUp,
        PickupRescheduled,
        Received,
        Stored,
        AssignedForDelivery,
        OutForDelivery,
        CalledConsignee,
        AssignedForReturn,
        Delivered,
        FailedDelivery,
        OnHold,
        OutForReturn,
        Returned,
        AssignedForTransfer,
        OutForTransfer,
        InTransit,
        Canceled,
        Lost,
        Owned,
        Released,
        ContainerSealed,
        ReceivedInTransit,
        ConsigneeInfoUpdated,
        MarkAsResolved,
        ReturnedToCustomer,
        MarkedAsLost,
        NoStatus,
        ChangedManually
    }
}
