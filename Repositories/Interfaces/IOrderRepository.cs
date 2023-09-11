using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Orders;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Orders;
using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IOrderRepository : ICrudRepository<Order>
    {
        Task<int> GetOrderNo();
        Task<Order> CancelOrder(int orderId, string note);
        Task<int> Delete(int Id, string note);
        Task<OrderDetail> CancelOrderDetail(int orderDetailId, string note);
        Task<PagingResponse<Order>> GetCancelledOrders(PagingRequestByWorkType pagingRequest);
        Task<PagingResponse<Order>> GetPendingOrders(PagingRequestByWorkType pagingRequest);
        Task<PagingResponse<Order>> SearchPendingOrders(SearchPagingRequest searchPagingRequest, DateTime fromDate, DateTime toDate);
        Task<PagingResponse<Order>> GetOrdersByCustomer(PagingRequest pagingRequest, int customerId, string orderStatus);
        Task<PagingResponse<Order>> GetOrdersBySalesman(PagingRequest pagingRequest, int salesmanId, string orderStatus);
        Task<PagingResponse<Order>> GetOrdersBySalesmanAndDateRange(PagingRequest pagingRequest, int salesmanId, DateTime fromDate, DateTime toDate);
        Task<PagingResponse<Order>> GetDeletedOrders(PagingRequestByWorkType pagingRequest);
        Task<PagingResponse<Order>> SearchCancelledOrders(SearchPagingRequest searchPagingRequest);
        Task<PagingResponse<Order>> SearchDeletedOrders(SearchPagingRequest searchPagingRequest);
        Task<PagingResponse<Order>> SearchByCustomer(SearchPagingRequest searchPagingRequest, int customerId);
        Task<PagingResponse<Order>> SearchBySalesman(SearchPagingRequest searchPagingRequest, int salesmanId);
        Task<PagingResponse<Order>> SearchWithFilter(OrderSearchPagingRequest searchPagingRequest);
        Task<PagingResponse<Order>> SearchBySalesmanAndDateRange(SearchPagingRequest searchPagingRequest, int salesmanId, DateTime fromDate, DateTime toDate);
        Task<PagingResponse<Order>> GetOrdersByDeliveryDate(DateTime fromDate, DateTime toDate, PagingRequest pagingReques);
        Task<List<OrderIdNumberResponse>> GetAllOrderNos();
        Task<bool> IsOrderNoExist(string OrderNo);
        Task<List<OrderDetail>> GetOrderDetails(string OrderNo);
        Task<PagingResponse<OrderDetail>> GetOrderAlerts(OrderAlertRequest orderAlertRequest);
        Task<PagingResponse<OrderDetail>> SearchOrderAlert(SearchPagingRequest searchPagingRequest, int daysBefore);
        Task<int> DeliveryPayment(DeliveryPaymentRequest deliveryPaymentRequest);
        Task<LastPaymentResponse> GetPaidAmountByCustomer(int orderId);
        Task<int> ChangeOrderStatus(int orderId, OrderStatusEnum orderStatus, string note, DateTime? DeliveredOn = null);
        Task<int> ChangeOrderDetailStatus(int orderDetailId, OrderStatusEnum orderStatus, string note, DateTime? DeliveredOn = null);
        Task<int> ChangeOrderDetailStatus(List<int> orderDetailIds, OrderStatusEnum orderStatus, string note, DateTime? DeliveredOn = null);
        Task<int> UpdateDesignModel(int orderDetailId, int modelId,string modelNo);
        Task<int> UpdateOrderAdvancePayment(List<CustomerAccountStatement> customerAccountStatements);
        Task<int> UpdateOrderDate(int orderId, DateTime orderDate);
        Task<int> UpdateOrderBalanceAmount(int ordeId, decimal paidAmount);
        Task<int> GetDesignSamplesCountInPreOrder(int customerId, int sampleId);
        Task<OrderQuantitiesResponse> GetOrderQuantities(DateTime fromDate, DateTime toDate);
        Task<PagingResponse<OrderDetail>> SearchFilterByWorkType(SearchPagingRequest searchPagingRequest, string workType,string orderStatus="active");
        Task<PagingResponse<OrderDetail>> FilterByWorkType(PagingRequestByWorkType pagingRequest);
        Task<int> UpdateModelNo(int orderDetailId, string modelNo);
        Task<int> UpdateWorkType(int orderDetailId, string workType);
        Task<int> UpdateOrderDescription(int orderDetailId, string description);
        Task<List<Order>> GetOrderNoByContactNo(string contactNo);
        Task<int> EditOrder(OrderEditRequest orderEditRequest);
        Task<int> GetNextTaxInvoiceNo(DateTime DeliveryDate);
        Task<PagingResponse<Order>> SearchOrdersByDeliveryDate(SearchPagingRequest searchPagingRequest);
        Task<List<OrderDetail>> GetUsedModelByContactNo(string contactNo);
        Task<Dictionary<string, int>> GetOrderCountByContactNo(List<string> contactNos);
        Task<OrderDetail> GetOrderDetails(int orderDetailId);

    }
}
