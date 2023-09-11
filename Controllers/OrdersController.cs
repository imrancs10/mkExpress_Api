using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Orders;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Orders;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerAccountStatementService _customerAccountStatementService;
        private readonly ICustomerMeasurementService _customerMeasurementService;
        private readonly IWorkTypeStatusService _workTypeStatusService;

        public OrdersController(IOrderService orderService, ICustomerAccountStatementService customerAccountStatementService, ICustomerMeasurementService customerMeasurementService, IWorkTypeStatusService workTypeStatusService)
        {
            _orderService = orderService;
            _customerAccountStatementService = customerAccountStatementService;
            _customerMeasurementService = customerMeasurementService;
            _workTypeStatusService = workTypeStatusService;
        }

        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.OrderPath)]
        public async Task<OrderResponse> AddOrder([FromBody] OrderRequest orderRequest)
        {
            return await _orderService.Add(orderRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.OrderDeletePath)]
        public async Task<int> DeleteOrder([FromRoute(Name = "id")] int orderId, [FromQuery] string note)
        {
            return await _orderService.Delete(orderId, note);
        }

        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderByIdPath)]
        public async Task<OrderResponse> GetOrder([FromRoute] int id)
        {
            return await _orderService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderPath)]
        public async Task<PagingResponse<OrderResponse>> GetAllOrder([FromQuery] PagingRequest pagingRequest)
        {
            return await _orderService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderSearchPath)]
        public async Task<PagingResponse<OrderResponse>> SearchOrder([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _orderService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderSearchCancelledOrdersPath)]
        public async Task<PagingResponse<OrderResponse>> SearchCancelledOrders([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _orderService.SearchCancelledOrders(searchPagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderSearchDeletedOrdersPath)]
        public async Task<PagingResponse<OrderResponse>> SearchDeletedOrders([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _orderService.SearchDeletedOrders(searchPagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderSearchByCustomersPath)]
        public async Task<PagingResponse<OrderResponse>> SearchByCustomer([FromQuery] SearchPagingRequest searchPagingRequest, [FromQuery] int customerId)
        {
            return await _orderService.SearchByCustomer(searchPagingRequest, customerId);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderSearchBySalesmanPath)]
        public async Task<PagingResponse<OrderResponse>> GetOrdersBySalesman([FromQuery] SearchPagingRequest searchPagingRequest, [FromQuery] int salesmanId)
        {
            return await _orderService.SearchBySalesman(searchPagingRequest, salesmanId);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderSearchBySalesmanAndDateRangePath)]
        public async Task<PagingResponse<OrderResponse>> SearchBySalesmanAndDateRange([FromQuery] SearchPagingRequest searchPagingRequest, [FromQuery] int salesmanId, [FromRoute] DateTime fromDate, [FromRoute] DateTime toDate)
        {
            return await _orderService.SearchBySalesmanAndDateRange(searchPagingRequest, salesmanId, fromDate, toDate);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetOrderBySalesmanAndDateRangePath)]
        public async Task<PagingResponse<OrderResponse>> OrderGetOrderBySalesmanAndDateRangePath([FromQuery] PagingRequest pagingRequest, [FromQuery] int salesmanId, [FromRoute] DateTime fromDate, [FromRoute] DateTime toDate)
        {
            return await _orderService.GetOrdersBySalesmanAndDateRange(pagingRequest, salesmanId, fromDate, toDate);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetOrderNoPath)]
        public async Task<int> GetOrderNo()
        {
            return await _orderService.GetOrderNo();
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderDetailCancelPath)]
        public async Task<int> CancelOrderDetail([FromQuery] int orderDetailId, [FromQuery] string note)
        {
            return await _orderService.CancelOrderDetail(orderDetailId, note);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderCancelPath)]
        public async Task<int> CancelOrder([FromQuery] int orderId, [FromQuery] string note)
        {
            return await _orderService.CancelOrder(orderId, note);
        }

        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetPreAmountPath)]
        public async Task<decimal> OrderGetPreAmount([FromQuery] int customerId, [FromQuery] int excludeOrderId = 0)
        {
            return await _customerAccountStatementService.GetCustomerBalanceAmount(customerId, excludeOrderId);
        }

        [ProducesResponseType(typeof(CustomerMeasurementResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetMeasurementPath)]
        public async Task<CustomerMeasurementResponse> GetCustomerMeasurement([FromQuery] int customerId)
        {
            return await _customerMeasurementService.GetMeasurement(customerId);
        }

        [ProducesResponseType(typeof(List<CustomerMeasurementResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetMeasurementsPath)]
        public async Task<List<CustomerMeasurementResponse>> GetCustomerMeasurements([FromQuery] string contactNo)
        {
            return await _customerMeasurementService.GetMeasurements(contactNo);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetCancelledOrderPath)]
        public async Task<PagingResponse<OrderResponse>> GetCancelledOrders([FromQuery] PagingRequestByWorkType pagingRequest)
        {
            return await _orderService.GetCancelledOrders(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetDeletedOrderPath)]
        public async Task<PagingResponse<OrderResponse>> GetDeletedOrders([FromQuery] PagingRequestByWorkType pagingRequest)
        {
            return await _orderService.GetDeletedOrders(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetOrderByDeliveryDatePath)]
        public async Task<PagingResponse<OrderResponse>> GetOrdersByDeliveryDate([FromQuery] PagingRequest pagingRequest, [FromRoute] DateTime fromDate, [FromRoute] DateTime toDate)
        {
            return await _orderService.GetOrdersByDeliveryDate(fromDate, toDate, pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderSearchOrderByDeliveryDatePath)]
        public async Task<PagingResponse<OrderResponse>> SearchOrdersByDeliveryDate([FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _orderService.SearchOrdersByDeliveryDate(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetOrderByCustomerPath)]
        public async Task<PagingResponse<OrderResponse>> GetOrdersByCustomer([FromQuery] PagingRequest pagingRequest, [FromQuery] int customerId,string orderStatus)
        {
            return await _orderService.GetOrdersByCustomer(pagingRequest, customerId,orderStatus);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetOrderBySalesmanPath)]
        public async Task<PagingResponse<OrderResponse>> GetOrdersBySalesman([FromQuery] PagingRequest pagingRequest, [FromQuery] int salesmanId, string orderStatus)
        {
            return await _orderService.GetOrdersBySalesman(pagingRequest, salesmanId,orderStatus);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderAlertResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<OrderAlertResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderSearchAlertPath)]
        public async Task<PagingResponse<OrderAlertResponse>> SearchOrderAlert([FromQuery] SearchPagingRequest pagingRequest, [FromQuery] int daysBefore = 10)
        {
            return await _orderService.SearchOrderAlert(pagingRequest, daysBefore);
        }

        [ProducesResponseType(typeof(List<OrderDetailResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<OrderDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetOrderDetailsPath)]
        public async Task<List<OrderDetailResponse>> GetOrderDetails([FromQuery] string orderNo)
        {
            return await _orderService.GetOrderDetails(orderNo);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderAlertResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<OrderAlertResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetOrderAlertsPath)]
        public async Task<PagingResponse<OrderAlertResponse>> GetOrderAlerts([FromQuery] OrderAlertRequest orderAlertRequest)
        {
            return await _orderService.GetOrderAlerts(orderAlertRequest);
        }

        [ProducesResponseType(typeof(List<OrderIdNumberResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<OrderIdNumberResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetOrderNosPath)]
        public async Task<List<OrderIdNumberResponse>> GetAllOrderNos()
        {
            return await _orderService.GetAllOrderNos();
        }

        [ProducesResponseType(typeof(List<WorkTypeStatusResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<WorkTypeStatusResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrdersWorkTypeStatusPath)]
        public async Task<List<WorkTypeStatusResponse>> GetWorkTypeStatuses([FromQuery] int orderDetailId)
        {
            return await _workTypeStatusService.GetByOrderDetailId(orderDetailId);
        }

        [ProducesResponseType(typeof(WorkTypeStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.OrdersWorkTypeStatusPath)]
        public async Task<WorkTypeStatusResponse> UpdateWorkTypeStatuses([FromBody] WorkTypeStatusRequest workTypeStatusRequests)
        {
            return await _workTypeStatusService.Update(workTypeStatusRequests);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.OrdersUpdateMeasurementPath)]
        public async Task<int> UpdateMeasuremnt([FromBody] List<UpdateMeasurementRequest> updateMeasurementRequest)
        {
            var result= await _customerMeasurementService.UpdateMeasurement(updateMeasurementRequest);
            if (updateMeasurementRequest.Count > 0)
                return await _orderService.UpdateOrderDescription(updateMeasurementRequest.First().OrderDetailId, updateMeasurementRequest.First().Description);
            return result;
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.OrdersUpdateModelNoPath)]
        public async Task<int> UpdateModelNo([FromQuery] int orderDetailId,[FromQuery] string modelNo)
        {
            return await _orderService.UpdateModelNo(orderDetailId,modelNo);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.OrdersUpdateDeliveryPaymentPath)]
        public async Task<int> UpdateDeliveryPayment([FromBody] DeliveryPaymentRequest deliveryPaymentRequest)
        {
            return await _orderService.DeliveryPayment(deliveryPaymentRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.OrdersUpdateDesignModelPath)]
        public async Task<int> UpdateDesignModel([FromRoute] int modelId, [FromRoute] int orderDetailId,[FromQuery] string modelName)
        {
            return await _orderService.UpdateDesignModel(orderDetailId, modelId, modelName);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.OrdersUpdateOrderDatePath)]
        public async Task<int> UpdateOrderDate([FromRoute] int orderId, [FromQuery] DateTime orderDate)
        {
            return await _orderService.UpdateOrderDate(orderId, orderDate);
        }

        [ProducesResponseType(typeof(LastPaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrdersGetPaidAmountByCustomerPath)]
        public async Task<LastPaymentResponse> GetPaidAmountByCustomer([FromQuery] int orderId)
        {
            return await _orderService.GetPaidAmountByCustomer(orderId);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrdersGetSampleCountInPreOrderPath)]
        public async Task<int> GetDesignSamplesCountInPreOrder([FromQuery] int customerId, [FromQuery] int sampleId)
        {
            return await _orderService.GetDesignSamplesCountInPreOrder(customerId, sampleId);
        }

        [ProducesResponseType(typeof(List<CustomerAdvancePaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrdersGetAdvancePaymentStatementPath)]
        public async Task<List<CustomerAdvancePaymentResponse>> GetAdvancePaymentStatement([FromQuery] int orderId)
        {
            return await _customerAccountStatementService.GetAdvancePaymentStatement(orderId);
        }

        [ProducesResponseType(typeof(OrderQuantitiesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrdersGetOrdersQtyPath)]
        public async Task<OrderQuantitiesResponse> GetOrderQuantities([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            return await _orderService.GetOrderQuantities(fromDate, toDate);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderGetPendingOrderPath)]
        public async Task<PagingResponse<OrderResponse>> GetOrderQuantities([FromQuery] PagingRequestByWorkType pagingRequest)
        {
            return await _orderService.GetPendingOrders(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderSearchPendingOrdersPath)]
        public async Task<PagingResponse<OrderResponse>> GetOrderQuantities([FromQuery] SearchPagingRequest pagingRequest, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            return await _orderService.SearchPendingOrders(pagingRequest,fromDate,toDate);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderSearchWithFilterPath)]
        public async Task<PagingResponse<OrderResponse>> SearchWithFilter([FromQuery] OrderSearchPagingRequest pagingRequest)
        {
            return await _orderService.SearchWithFilter(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderDetailsGetByWorkTypePath)]
        public async Task<PagingResponse<OrderDetailResponse>> FilterByWorkType([FromQuery] PagingRequestByWorkType pagingRequest)
        {
            return await _orderService.FilterByWorkType(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<OrderDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderDetailsSearchByWorkTypePath)]
        public async Task<PagingResponse<OrderDetailResponse>> SearchFilterByWorkType([FromQuery] SearchPagingRequest pagingRequest, [FromQuery] string workType,[FromQuery] string orderStatus = "active")
        {
            return await _orderService.SearchFilterByWorkType(pagingRequest, workType,orderStatus);
        } 
        
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrdersGetOrderStatusListPath)]
        public List<string> GetOrderStatusList()
        {
             return _orderService.GetOrderStatusList();
        }

        [ProducesResponseType(typeof(List<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrdersGetOrderNoByContactPath)]
        public async Task<List<OrderResponse>> GetOrderNoByContactNo([FromQuery]string contactNo)
        {
            return await _orderService.GetOrderNoByContactNo(contactNo);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.OrdersEditPath)]
        public async Task<int> EditOrder([FromBody] OrderEditRequest orderEditRequest)
        {
            return await _orderService.EditOrder(orderEditRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.OrdersUpdateStatementPath)]
        public async Task<int> UpdateStatement([FromBody] CustomerAdvancePaymentRequest request)
        {
            return await _customerAccountStatementService.UpdateStatement(request);
        }

        [ProducesResponseType(typeof(List<OrderDetailDesignModelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrdersGetModalNoByContactPath)]
        public async Task<List<OrderDetailDesignModelResponse>> GetUsedModelByContactNo([FromQuery] string contactNo)
        {
            return await _orderService.GetUsedModelByContactNo(contactNo);
        }

        [ProducesResponseType(typeof(OrderDetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OrderDetailResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.OrderDetailByIdPath)]
        public async Task<OrderDetailResponse> GetOrderDetails([FromRoute] int id)
        {
            return await _orderService.GetOrderDetails(id);
        }
    }
}
