using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Orders;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IOrderService : ICrudService<OrderRequest, OrderResponse>
    {
        Task<int> GetOrderNo();
        Task<int> CancelOrder(int orderId, string note);
        Task<int> CancelOrderDetail(int orderDetailId, string note);
        Task<PagingResponse<OrderResponse>> GetCancelledOrders(PagingRequestByWorkType pagingRequest);
        Task<PagingResponse<OrderResponse>> GetPendingOrders(PagingRequestByWorkType pagingRequest);
        Task<PagingResponse<OrderResponse>> SearchPendingOrders(SearchPagingRequest searchPagingRequest, DateTime fromDate, DateTime toDate);
        Task<PagingResponse<OrderResponse>> GetDeletedOrders(PagingRequestByWorkType pagingRequest);
        Task<PagingResponse<OrderResponse>> GetOrdersBySalesmanAndDateRange(PagingRequest pagingRequest, int salesmanId, DateTime fromDate, DateTime toDate);
        Task<PagingResponse<OrderResponse>> SearchCancelledOrders(SearchPagingRequest searchPagingRequest);
        Task<PagingResponse<OrderResponse>> SearchDeletedOrders(SearchPagingRequest searchPagingRequest);
        Task<PagingResponse<OrderResponse>> SearchByCustomer(SearchPagingRequest searchPagingRequest, int customerId);
        Task<PagingResponse<OrderResponse>> SearchBySalesman(SearchPagingRequest searchPagingRequest, int salesmanId);
        Task<PagingResponse<OrderResponse>> SearchBySalesmanAndDateRange(SearchPagingRequest searchPagingRequest, int salesmanId, DateTime fromDate, DateTime toDate);
        Task<PagingResponse<OrderResponse>> GetOrdersByDeliveryDate(DateTime fromDate, DateTime toDate, PagingRequest pagingRequest);
        Task<int> Delete(int id, string note);
        Task<PagingResponse<OrderResponse>> GetOrdersByCustomer(PagingRequest pagingRequest, int customerId, string orderStatus);
        Task<PagingResponse<OrderResponse>> GetOrdersBySalesman(PagingRequest pagingRequest, int salesmanId, string orderStatus);
        Task<List<OrderIdNumberResponse>> GetAllOrderNos();
        Task<List<OrderDetailResponse>> GetOrderDetails(string OrderNo);
        Task<PagingResponse<OrderAlertResponse>> GetOrderAlerts(OrderAlertRequest orderAlertRequest);
        Task<PagingResponse<OrderAlertResponse>> SearchOrderAlert(SearchPagingRequest searchPagingRequest, int daysBefore);
        Task<int> DeliveryPayment(DeliveryPaymentRequest deliveryPaymentRequest);
        Task<LastPaymentResponse> GetPaidAmountByCustomer(int orderId);
        Task<int> UpdateDesignModel(int orderDetailId, int modelId,string modelName);
        Task<int> UpdateOrderDate(int orderId, DateTime orderDate);
        Task<int> GetDesignSamplesCountInPreOrder(int customerId, int sampleId);
        Task<OrderQuantitiesResponse> GetOrderQuantities(DateTime fromDate, DateTime toDate);
        Task<PagingResponse<OrderResponse>> SearchWithFilter(OrderSearchPagingRequest searchPagingRequest);
        Task<PagingResponse<OrderDetailResponse>> SearchFilterByWorkType(SearchPagingRequest searchPagingRequest, string workType, string orderStatus = "active");
        Task<PagingResponse<OrderDetailResponse>> FilterByWorkType(PagingRequestByWorkType pagingRequest);
        List<string> GetOrderStatusList();
        Task<int> UpdateModelNo(int orderDetailId, string modelNo);
        Task<int> UpdateOrderDescription(int orderDetailId, string description);
        Task<List<OrderResponse>> GetOrderNoByContactNo(string contactNo);
        Task<int> EditOrder(OrderEditRequest orderEditRequest);
        Task<PagingResponse<OrderResponse>> SearchOrdersByDeliveryDate(SearchPagingRequest searchPagingRequest);
        Task<List<OrderDetailDesignModelResponse>> GetUsedModelByContactNo(string contactNo);
        Task<OrderDetailResponse> GetOrderDetails(int orderDetailId);
    }
}
