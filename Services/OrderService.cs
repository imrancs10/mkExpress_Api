using AutoMapper;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Orders;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Orders;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDesignSampleService _designSampleService;
        private readonly ICustomerMeasurementRepository _customerMeasurementRepository;
        private readonly IMapper _mapper;
        private readonly IWorkTypeStatusRepository _workTypeStatusRepository;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, ICustomerMeasurementRepository customerMeasurementRepository, IDesignSampleService designSampleService, IWorkTypeStatusRepository workTypeStatusRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _customerMeasurementRepository = customerMeasurementRepository;
            _designSampleService = designSampleService;
            _workTypeStatusRepository = workTypeStatusRepository;
        }
        public async Task<OrderResponse> Add(OrderRequest orderRequest)
        {
            if (orderRequest == null || orderRequest.OrderDetails.Count == 0)
            {
                throw new BusinessRuleViolationException(StaticValues.OrderDetailError, StaticValues.OrderDetailMessage);
            }

            if(orderRequest.OrderDate>DateTime.Now.Date)
            {
                throw new BusinessRuleViolationException(StaticValues.OrderDateInFutureError, StaticValues.OrderDateInFutureMessage);
            }
            if (orderRequest.OrderDate > orderRequest.OrderDeliveryDate)
            {
                throw new BusinessRuleViolationException(StaticValues.OrderDeliveryDateError, StaticValues.OrderDeliveryDateMessage);
            }

            if (await _orderRepository.IsOrderNoExist(orderRequest.OrderNo))
            {
                throw new BusinessRuleViolationException(StaticValues.OrderNoAlreadyExistError, StaticValues.OrderNoAlreadyExistMessage);
            }

            Order order = _mapper.Map<Order>(orderRequest);
            order.Status = OrderStatusEnum.Active.ToString();

            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.Status = OrderStatusEnum.Active.ToString();
                if (orderDetail.DesignSampleId == 0)
                {
                    orderDetail.DesignSampleId = null;
                }
            }

            var result = _mapper.Map<OrderResponse>(await _orderRepository.Add(order));
            if (result != null)
            {
                List<CustomerMeasurement> customerMeasurement = _mapper.Map<List<CustomerMeasurement>>(order.OrderDetails);
                customerMeasurement.ForEach(x =>
                {
                    x.CustomerId = order.CustomerId;
                    x.Id = 0;
                });
                await _customerMeasurementRepository.AddUpdateMeasurement(customerMeasurement);
            }

            await AddWorkOrderStatus(result.OrderDetails, orderRequest.OrderDetails);

            //prevent cycle dependency
            result.AccountStatements = null;
            return result;

        }

        public async Task<int> CancelOrder(int orderId, string note)
        {
            if (await _workTypeStatusRepository.IsAnyKandooraProcessing(orderId))
            {
                throw new BusinessRuleViolationException(StaticValues.OrderIsProceesingError, StaticValues.OrderIsProceesingMessage);
            }

            var order = await _orderRepository.CancelOrder(orderId, note);
            if (order == null)
                return default;
            //await AddSampleQuantity(order, 1);
            return order.Id;
        }

        public async Task<int> CancelOrderDetail(int orderDetailId, string note)
        {
            if (await _workTypeStatusRepository.IsKandooraProcessing(orderDetailId))
            {
                throw new BusinessRuleViolationException(StaticValues.OrderIsProceesingError, StaticValues.OrderIsProceesingMessage);
            }

            var orderDetail = await _orderRepository.CancelOrderDetail(orderDetailId, note);
            if (orderDetail == null)
                return default;
            //await _designSampleService.AddQuantity(1, orderDetail.DesignSampleId == null ? 0 : (int)orderDetail.DesignSampleId);
            return orderDetail.Id;
        }

        public async Task<int> Delete(int orderId)
        {
            if (await _workTypeStatusRepository.IsAnyKandooraProcessing(orderId))
            {
                throw new BusinessRuleViolationException(StaticValues.OrderIsProceesingError, StaticValues.OrderIsProceesingMessage);
            }

            Order order = await _orderRepository.Get(orderId);
            await AddSampleQuantity(order, -1);
            return await _orderRepository.Delete(orderId);
        }

        public async Task<int> Delete(int id, string note)
        {
            if (await _workTypeStatusRepository.IsAnyKandooraProcessing(id))
            {
                throw new BusinessRuleViolationException(StaticValues.OrderIsProceesingError, StaticValues.OrderIsProceesingMessage);
            }

            // Order order = await _orderRepository.Get(id);
            // await AddSampleQuantity(order, -1);
            return await _orderRepository.Delete(id, note);
        }

        public async Task<int> DeliveryPayment(DeliveryPaymentRequest deliveryPaymentRequest)
        {
            return await _orderRepository.DeliveryPayment(deliveryPaymentRequest);
        }

        public async Task<OrderResponse> Get(int orderId)
        {
            var res = _mapper.Map<OrderResponse>(await _orderRepository.Get(orderId));
            res.AccountStatements.ForEach(ele =>
            {
                ele.Order = null;
            });
            return res;
        }

        public async Task<PagingResponse<OrderResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.GetAll(pagingRequest));
        }

        public async Task<List<OrderIdNumberResponse>> GetAllOrderNos()
        {
            return await _orderRepository.GetAllOrderNos();
        }

        public async Task<PagingResponse<OrderResponse>> GetCancelledOrders(PagingRequestByWorkType pagingRequest)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.GetCancelledOrders(pagingRequest));
        }

        public async Task<PagingResponse<OrderResponse>> GetDeletedOrders(PagingRequestByWorkType pagingRequest)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.GetDeletedOrders(pagingRequest));
        }

        public async Task<PagingResponse<OrderAlertResponse>> GetOrderAlerts(OrderAlertRequest orderAlertRequest)
        {
            var result= await _orderRepository.GetOrderAlerts(orderAlertRequest);
            PagingResponse<OrderAlertResponse> pagingResponse = new PagingResponse<OrderAlertResponse>();
            pagingResponse.Data = OrderAlertMapper(result.Data);
            pagingResponse.PageNo = result.PageNo;
            pagingResponse.PageSize = result.PageSize;
            pagingResponse.TotalRecords = result.TotalRecords;
            return pagingResponse;
        }

        public async Task<List<OrderDetailResponse>> GetOrderDetails(string orderNo)
        {
            return _mapper.Map<List<OrderDetailResponse>>(await _orderRepository.GetOrderDetails(orderNo));
        }

        public async Task<int> GetOrderNo()
        {
            return await _orderRepository.GetOrderNo();
        }

        public async Task<PagingResponse<OrderResponse>> GetOrdersByCustomer(PagingRequest pagingRequest, int customerId,string orderStatus)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.GetOrdersByCustomer(pagingRequest, customerId, orderStatus));
        }

        public async Task<PagingResponse<OrderResponse>> GetOrdersByDeliveryDate(DateTime fromDate, DateTime toDate, PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.GetOrdersByDeliveryDate(fromDate, toDate, pagingRequest));
        }

        public async Task<PagingResponse<OrderResponse>> GetOrdersBySalesman(PagingRequest pagingRequest, int salesmanId,string orderStatus)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.GetOrdersBySalesman(pagingRequest, salesmanId,orderStatus));
        }

        public async Task<PagingResponse<OrderResponse>> GetOrdersBySalesmanAndDateRange(PagingRequest pagingRequest, int salesmanId, DateTime fromDate, DateTime toDate)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.GetOrdersBySalesmanAndDateRange(pagingRequest, salesmanId, fromDate, toDate));
        }

        public async Task<LastPaymentResponse> GetPaidAmountByCustomer(int orderId)
        {
            return await _orderRepository.GetPaidAmountByCustomer(orderId);
        }

        public async Task<PagingResponse<OrderResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.Search(searchPagingRequest));
        }

        public async Task<PagingResponse<OrderResponse>> SearchByCustomer(SearchPagingRequest searchPagingRequest, int customerId)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.SearchByCustomer(searchPagingRequest, customerId));
        }

        public async Task<PagingResponse<OrderResponse>> SearchBySalesman(SearchPagingRequest searchPagingRequest, int salesmanId)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.SearchBySalesman(searchPagingRequest, salesmanId));
        }

        public async Task<PagingResponse<OrderResponse>> SearchBySalesmanAndDateRange(SearchPagingRequest searchPagingRequest, int salesmanId, DateTime fromDate, DateTime toDate)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.SearchBySalesmanAndDateRange(searchPagingRequest, salesmanId, fromDate, toDate));
        }

        public async Task<PagingResponse<OrderResponse>> SearchCancelledOrders(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.SearchCancelledOrders(searchPagingRequest));
        }

        public async Task<PagingResponse<OrderResponse>> SearchDeletedOrders(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.SearchDeletedOrders(searchPagingRequest));
        }

        public async Task<PagingResponse<OrderAlertResponse>> SearchOrderAlert(SearchPagingRequest searchPagingRequest, int daysBefore)
        {
           // return _mapper.Map<PagingResponse<OrderDetailResponse>>();
            var result = await _orderRepository.SearchOrderAlert(searchPagingRequest, daysBefore);
            PagingResponse<OrderAlertResponse> pagingResponse = new PagingResponse<OrderAlertResponse>();
            pagingResponse.Data = OrderAlertMapper(result.Data);
            pagingResponse.PageNo = result.PageNo;
            pagingResponse.PageSize = result.PageSize;
            pagingResponse.TotalRecords = result.TotalRecords;
            return pagingResponse;
        }

        public async Task<OrderResponse> Update(OrderRequest orderRequest)
        {
            return default;
        }

        public async Task<int> UpdateDesignModel(int orderDetailId, int modelId, string modelNo)
        {
            var result = await _orderRepository.UpdateDesignModel(orderDetailId, modelId, modelNo);
            Dictionary<int, int> sampleQuantity = new Dictionary<int, int>
            {
                { modelId, -1 }
            };
            await _designSampleService.AddQuantity(sampleQuantity);
            return result;
        }

        public async Task<int> UpdateOrderDate(int orderId, DateTime orderDate)
        {
            return await _orderRepository.UpdateOrderDate(orderId, orderDate);
        }

        private async Task AddSampleQuantity(Order order, int addOrSubstract)
        {
            if (order != null && order.OrderDetails != null && order.OrderDetails.Count > 0)
            {

                List<int?> sampleIds = order.OrderDetails.Select(x => x.DesignSampleId == null ? 0 : x.DesignSampleId).Distinct().ToList();
                Dictionary<int, int> sampleQuantity = new Dictionary<int, int>();
                foreach (int sampleId in sampleIds)
                {
                    int quantity = order.OrderDetails.Where(x => x.DesignSampleId == sampleId).Count();
                    sampleQuantity.Add(sampleId, quantity * addOrSubstract);
                }
                await _designSampleService.AddQuantity(sampleQuantity);
            }
        }

        private async Task AddWorkOrderStatus(List<OrderDetailResponse> orderDetails, List<OrderDetailRequest> orderDetailRequests)
        {
            if (orderDetails == null || orderDetails.Count == 0)
            {
                throw new BusinessRuleViolationException(StaticValues.OrderDetailNotFoundError, StaticValues.OrderDetailNotFoundMessage);
            }
            List<WorkTypeStatus> workTypeStatuses = new List<WorkTypeStatus>();
            Dictionary<string, OrderDetailRequest> orderDetailDictionary = orderDetailRequests.ToDictionary(x => x.OrderNo, y => y);
            foreach (OrderDetailResponse orderDetail in orderDetails)
            {
                List<WorkTypeStatus> workTypes = orderDetailDictionary[orderDetail.OrderNo]
                    .WorkTypes
                    .Select(x => new WorkTypeStatus()
                    {
                        OrderDetailId = orderDetail.Id,
                        WorkTypeId = x
                    }).ToList();
                workTypeStatuses.AddRange(workTypes);
            }
            await _workTypeStatusRepository.Add(workTypeStatuses);
        }
        
        public async Task<int> GetDesignSamplesCountInPreOrder(int customerId, int sampleId)
        {
            return await _orderRepository.GetDesignSamplesCountInPreOrder(customerId, sampleId);
        }

        public async Task<OrderQuantitiesResponse> GetOrderQuantities(DateTime fromDate, DateTime toDate)
        {
            return await _orderRepository.GetOrderQuantities(fromDate, toDate);
        }

        public async Task<PagingResponse<OrderResponse>> GetPendingOrders(PagingRequestByWorkType pagingRequest)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.GetPendingOrders(pagingRequest));
        }

        public async Task<PagingResponse<OrderResponse>> SearchPendingOrders(SearchPagingRequest searchPagingRequest, DateTime fromDate, DateTime toDate)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.SearchPendingOrders(searchPagingRequest, fromDate, toDate));
        }

        public async Task<PagingResponse<OrderResponse>> SearchWithFilter(OrderSearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.SearchWithFilter(searchPagingRequest));
        }

        public async Task<PagingResponse<OrderDetailResponse>> SearchFilterByWorkType(SearchPagingRequest searchPagingRequest, string workType, string orderStatus = "active")
        {
            return _mapper.Map<PagingResponse<OrderDetailResponse>>(await _orderRepository.SearchFilterByWorkType(searchPagingRequest, workType, orderStatus));
        }

        public async Task<PagingResponse<OrderDetailResponse>> FilterByWorkType(PagingRequestByWorkType pagingRequest)
        {
            return _mapper.Map<PagingResponse<OrderDetailResponse>>(await _orderRepository.FilterByWorkType(pagingRequest));
        }

        public List<string> GetOrderStatusList()
        {
            return Enum.GetValues(typeof(OrderStatusEnum)).Cast<OrderStatusEnum>().Select(x => x.ToString()).ToList();
        }

        public async Task<int> UpdateModelNo(int orderDetailId, string modelNo)
        {
            return await _orderRepository.UpdateModelNo(orderDetailId, modelNo);
        }

        public async Task<int> UpdateOrderDescription(int orderDetailId, string description)
        {
            return await _orderRepository.UpdateOrderDescription(orderDetailId, description);
        }

        public async Task<List<OrderResponse>> GetOrderNoByContactNo(string contactNo)
        {
            return _mapper.Map<List<OrderResponse>>(await _orderRepository.GetOrderNoByContactNo(contactNo));
        }

        public async Task<int> EditOrder(OrderEditRequest orderEditRequest)
        {
            return await _orderRepository.EditOrder(orderEditRequest);
        }

        private List<OrderAlertResponse> OrderAlertMapper(List<OrderDetail> orderDetails)
        {
            List<OrderAlertResponse> orderAlertResponses = new List<OrderAlertResponse>();
           

            orderDetails.ForEach(res =>
            {
                var newAlert = new OrderAlertResponse()
                {
                    OrderNo = res.Order.OrderNo,
                    KandooraNo = res.OrderNo,
                    DeliveryDate = res.Order.OrderDeliveryDate,
                    SubTotalAmount = res.SubTotalAmount,
                    OrderDetailId = res.Id,
                    OrderId = res.OrderId,
                    Status = res.Status,
                    OrderQty=(int)res.Order.Qty,
                    Salesman=res.Order.Employee.FirstName+" "+res.Order.Employee.LastName
                };
                res.WorkTypeStatuses = res.WorkTypeStatuses.Where(x => !x.IsDeleted).ToList();
                res.WorkTypeStatuses.ForEach(wtRes =>
                {
                    string wType = wtRes.WorkType.Value.Trim().Replace(" ", string.Empty);
                    if (wType == WorkTypeEnum.Designing.ToString())
                    {
                        newAlert.Design = wtRes.IsSaved ? StaticValues.WorkTypeStatusDone : StaticValues.WorkTypeStatusNotDone;
                    }
                    if (wType == WorkTypeEnum.Apliq.ToString())
                    {
                        newAlert.Apliq = wtRes.IsSaved ? StaticValues.WorkTypeStatusDone : StaticValues.WorkTypeStatusNotDone;
                    }
                    if (wType == WorkTypeEnum.CrystalUsed.ToString())
                    {
                        newAlert.HFix = wtRes.IsSaved || wtRes.Price > 0 || wtRes.Extra > 0 ? StaticValues.WorkTypeStatusDone : StaticValues.WorkTypeStatusNotDone;
                    }
                    if (wType == WorkTypeEnum.Cutting.ToString())
                    {
                        newAlert.Cutting = wtRes.IsSaved  ? StaticValues.WorkTypeStatusDone : StaticValues.WorkTypeStatusNotDone;
                    }
                    if (wType == WorkTypeEnum.HandEmbroidery.ToString())
                    {
                        newAlert.HEmb = wtRes.IsSaved ? StaticValues.WorkTypeStatusDone : StaticValues.WorkTypeStatusNotDone;
                    }
                    if (wType == WorkTypeEnum.MachineEmbroidery.ToString())
                    {
                        newAlert.MEmb = wtRes.IsSaved ? StaticValues.WorkTypeStatusDone : StaticValues.WorkTypeStatusNotDone;
                    }
                    if (wType == WorkTypeEnum.Stitching.ToString())
                    {
                        newAlert.Stitch = wtRes.IsSaved ? StaticValues.WorkTypeStatusDone : StaticValues.WorkTypeStatusNotDone;
                    }
                });
                orderAlertResponses.Add(newAlert);
            });
            return orderAlertResponses;
        }

        public async Task<PagingResponse<OrderResponse>> SearchOrdersByDeliveryDate(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<OrderResponse>>(await _orderRepository.SearchOrdersByDeliveryDate(searchPagingRequest));
        }

        public async Task<List<OrderDetailDesignModelResponse>> GetUsedModelByContactNo(string contactNo)
        {
            var result = await _orderRepository.GetUsedModelByContactNo(contactNo);
            List<OrderDetailDesignModelResponse> output = new List<OrderDetailDesignModelResponse>();
            result.ForEach(res =>
            {
                var lstItem = new OrderDetailDesignModelResponse()
                {
                    Id = res.Id,
                    OrderId = res.OrderId,
                    OrderNo = res.OrderNo,
                };
                lstItem.ModelNo = res.DesignSampleId >0 ? res.DesignSample?.Model ?? string.Empty:(res.ModelNo == null ? string.Empty : res.ModelNo);
                lstItem.Value = $"{res.OrderNo} - {lstItem.ModelNo}";
                output.Add(lstItem);
            });
            return output;
        }

        public async Task<OrderDetailResponse> GetOrderDetails(int orderDetailId)
        {
            return _mapper.Map<OrderDetailResponse>(await _orderRepository.GetOrderDetails(orderDetailId));
        }
    }
}
