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
            #region Order
            CreateMap<OrderRequest, Order>();
            CreateMap<OrderDetail, CustomerMeasurement>();
            CreateMap<OrderDetailRequest, CustomerMeasurement>();
            CreateMap<OrderDetailRequest, OrderDetail>();
            CreateMap<Order, OrderResponse>()
                 .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.Firstname ?? ""} {src.Customer.Lastname ?? ""}"))
                 .ForMember(dest => dest.Contact1, opt => opt.MapFrom(src => src.Customer.Contact1))
                 .ForMember(dest => dest.CustomerTRN, opt => opt.MapFrom(src => src.Customer.TRN))
                 .ForMember(dest => dest.BalanceAmount, opt => opt.MapFrom(src => src.BalanceAmount<0?0:src.BalanceAmount))
                 .ForMember(dest => dest.Salesman, opt => opt.MapFrom(src => $"{src.Employee.FirstName ?? ""} {src.Employee.LastName ?? ""}"))
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => $"{src.EmployeeCreated.FirstName ?? ""} {src.EmployeeCreated.LastName ?? ""}"))
                 .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => $"{src.EmployeeUpdated.FirstName ?? ""} {src.EmployeeUpdated.LastName ?? ""}"));

            CreateMap<OrderDetail, OrderDetailResponse>()
                 .ForMember(dest => dest.DesignCategory, opt => opt.MapFrom(src => src.DesignSample.MasterDesignCategory.Value ?? ""))
                 .ForMember(dest => dest.DesignModel, opt => opt.MapFrom(src => src.ModelNo!=null?src.ModelNo:src.DesignSample.Model??""))
                 .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Order.Customer.Firstname ?? ""} {src.Order.Customer.Lastname ?? ""}"))
                  .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Order.Customer.Contact1))
                   .ForMember(dest => dest.MainOrderNo, opt => opt.MapFrom(src => src.Order.OrderNo))
                   .ForMember(dest => dest.OrderQty, opt => opt.MapFrom(src => src.Order.Qty))
                     .ForMember(dest => dest.Contact1, opt => opt.MapFrom(src => src.Order.Customer.Contact1))
                        .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Order.Customer.Contact1))
                    .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.Order.OrderDate))
                      .ForMember(dest => dest.Salesman, opt => opt.MapFrom(src => $"{src.Order.Employee.FirstName ?? ""} {src.Order.Employee.LastName ?? ""}"))
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => $"{src.EmployeeCreated.FirstName ?? ""} {src.EmployeeCreated.LastName ?? ""}"))
                 .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => $"{src.EmployeeUpdated.FirstName ?? ""} {src.EmployeeUpdated.LastName ?? ""}"));
            CreateMap<PagingResponse<Order>, PagingResponse<OrderResponse>>();
            CreateMap<PagingResponse<OrderDetail>, PagingResponse<OrderDetailResponse>>();
            CreateMap<UpdateMeasurementRequest, OrderDetail>();
            CreateMap<OrderSearchPagingRequest, SearchPagingRequest>();
            CreateMap<OrderDetail, OrderDetailDesignModelResponse>();
            #endregion

            #region Purchase Entry
            CreateMap<PurchaseEntryRequest, PurchaseEntry>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PurchaseEntryId));
            CreateMap<PurchaseEntryDetailRequest, PurchaseEntryDetail>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PurchaseEntryDetailId));
            CreateMap<PurchaseEntry, PurchaseEntryResponse>()
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier.CompanyName))
                 .ForMember(dest => dest.TRN, opt => opt.MapFrom(src => src.Supplier.TRN))
                 .ForMember(dest => dest.ContactNo, opt => opt.MapFrom(src => src.Supplier.Contact))
                .ForMember(dest => dest.PurchaseEntryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => $"{src.EmployeeCreatedBy.FirstName} {src.EmployeeCreatedBy.LastName}"))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Supplier.CompanyName));

            CreateMap<PurchaseEntryDetail, PurchaseEntryDetailResponse>()
                 .ForMember(dest => dest.PurchaseEntryDetailId, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));

            CreateMap<PagingResponse<PurchaseEntry>, PagingResponse<PurchaseEntryResponse>>();
            CreateMap<ProductStock, OrderCrystalResponse>()
                  .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.ProductName))
                   .ForMember(dest => dest.Shape, opt => opt.MapFrom(src => src.Product.FabricWidth.Value))
                    .ForMember(dest => dest.ProductStockId, opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Product.Size.Value));
            #endregion

            #region Kandoora
            CreateMap<EachKandooraExpenseRequest, EachKandooraExpense>();
            CreateMap<EachKandooraExpense, EachKandooraExpenseResponse>();
            CreateMap<PagingResponse<EachKandooraExpense>, PagingResponse<EachKandooraExpenseResponse>>();

            CreateMap<EachKandooraExpenseHeadRequest, EachKandooraExpenseHead>();
            CreateMap<EachKandooraExpenseHead, EachKandooraExpenseHeadResponse>();
            CreateMap<PagingResponse<EachKandooraExpenseHead>, PagingResponse<EachKandooraExpenseHeadResponse>>();
            #endregion

            #region Master Data

            CreateMap<JobTitleRequest, MasterJobTitle>();
            CreateMap<PagingResponse<MasterData>, PagingResponse<MasterDataResponse>>();
            CreateMap<PagingResponse<MasterDataType>, PagingResponse<MasterDataTypeResponse>>();
            CreateMap<MasterDataRequest, MasterData>();
            CreateMap<MasterDataTypeRequest, MasterDataType>();
            CreateMap<MasterDataType, MasterDataTypeResponse>();
            CreateMap<MasterJobTitle, JobTitleResponse>();
            CreateMap<DesignSampleRequest, DesignSample>();
            CreateMap<DesignCategoryRequest, MasterDesignCategory>();
            CreateMap<PagingResponse<DesignSample>, PagingResponse<DesignSampleResponse>>();
            CreateMap<MasterDesignCategory, DesignCategoryResponse>();
            CreateMap<PagingResponse<Supplier>, PagingResponse<SupplierResponse>>();

            CreateMap<SupplierRequest, Supplier>();
            CreateMap<Supplier, SupplierResponse>();
            CreateMap<Dropdown<Supplier>, DropdownResponse<SupplierResponse>>();
            #endregion

            #region Work Type Status
            CreateMap<WorkTypeStatusRequest, WorkTypeStatus>();
            CreateMap<WorkTypeStatus, WorkTypeStatusResponse>()
                    .ForMember(dest => dest.CompletedByName, opt => opt.MapFrom(src => $"{src.CompletedByEmployee.FirstName ?? ""} {src.CompletedByEmployee.LastName ?? ""}"))
                    .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => $"{src.CreatedByEmployee.FirstName ?? ""} {src.CreatedByEmployee.LastName ?? ""}"))
                    .ForMember(dest => dest.WorkType, opt => opt.MapFrom(src => src.WorkType.Value))
                    .ForMember(dest => dest.WorkTypeSequence, opt => opt.MapFrom(src => src.WorkType.Code))
                    .ForMember(dest => dest.KandooraNo, opt => opt.MapFrom(src => src.OrderDetail.OrderNo))
                     .ForMember(dest => dest.DeliveredDate, opt => opt.MapFrom(src => src.OrderDetail.DeliveredDate))
              .ForMember(dest => dest.OrderNo, opt => opt.MapFrom(src => src.OrderDetail.Order.OrderNo));
            #endregion

            #region Employee Advance Payment
            CreateMap<EmployeeAdvancePaymentRequest, EmployeeAdvancePayment>();
            CreateMap<EmployeeAdvancePayment, EmployeeAdvancePaymentResponse>();
            CreateMap<PagingResponse<EmployeeAdvancePayment>, PagingResponse<EmployeeAdvancePaymentResponse>>();
            CreateMap<EmployeeEMIPayment, EmployeeEMIPaymentResponse>();
            #endregion

            #region Customer
            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
            CreateMap<CustomerAccountStatement, CustomerAdvancePaymentResponse>();
            CreateMap<CustomerMeasurement, CustomerMeasurementResponse>();
            CreateMap<PagingResponse<Customer>, PagingResponse<CustomerResponse>>();
            CreateMap<UpdateMeasurementRequest, CustomerMeasurement>();
            CreateMap<CustomerAccountStatement, CustomerAccountStatementResponse>();
            #endregion

            CreateMap<UserRegistrationRequest, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UserResponse, User>();

            CreateMap<Dropdown, DropdownResponse>();
            CreateMap<PagingResponse<MasterJobTitle>, PagingResponse<JobTitleResponse>>();

            #region Employee
            CreateMap<MonthlyAttendenceRequest, MonthlyAttendence>();
            CreateMap<PagingResponse<MonthlyAttendence>, PagingResponse<MonthlyAttendenceResponse>>();

            CreateMap<PagingResponse<Employee>, PagingResponse<EmployeeResponse>>();
            CreateMap<PagingResponse<MasterDesignCategory>, PagingResponse<DesignCategoryResponse>>();
            CreateMap<Dropdown<Employee>, DropdownResponse<EmployeeResponse>>();
            CreateMap<Employee, EmployeeResponse>()
             .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.MasterJobTitle.Value))
             .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRole.Name));

            CreateMap<EmployeeRequest, Employee>();


            CreateMap<MonthlyAttendence, MonthlyAttendenceResponse>()
               .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
               .ForMember(dest => dest.EmployeeAdvancePayments, opt => opt.MapFrom(src => src.Employee.EmployeeAdvancePayments));

            #endregion

            #region Custome Map

            CreateMap<DesignSample, DesignSampleResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.MasterDesignCategory.Value));
            CreateMap<MasterData, MasterDataResponse>()
                .ForMember(dest => dest.MasterDataTypeCode, opt => opt.MapFrom(src => src.MasterDataType));

            CreateMap<CustomerAdvancePaymentRequest, CustomerAccountStatement>();
            #endregion

            #region Permission
            CreateMap<ResourceTypeRequest, ResourceType>();
            CreateMap<ResourceType, ResourceTypeResponse>();

            CreateMap<PermissionResourceRequest, PermissionResource>();
            CreateMap<PermissionResource, PermissionResourceResponse>()
                 .ForMember(dest => dest.ResourceTypeName, opt => opt.MapFrom(src => src.ResourceType.Name));

            CreateMap<UserPermissionRequest, UserPermission>();
            CreateMap<UserPermission, UserPermissionResponse>()
                 .ForMember(dest => dest.PermissionResourceType, opt => opt.MapFrom(src => src.PermissionResource.ResourceType.Name))
                 .ForMember(dest => dest.PermissionResourceName, opt => opt.MapFrom(src => src.PermissionResource.Name))
                 .ForMember(dest => dest.PermissionResourceCode, opt => opt.MapFrom(src => src.PermissionResource.Code));

            CreateMap<UserRole, UserRoleResponse>()
                .ForMember(dest => dest.UserRoleId, opt => opt.MapFrom(src => src.Id));
            #endregion

            #region Holiday
            CreateMap<MasterHolidayRequest, MasterHoliday>();
            CreateMap<MasterDataTypeRequest, MasterHolidayType>();
            CreateMap<MasterHolidayNameRequest, MasterHolidayName>();
            CreateMap<MasterHoliday, MasterHolidayResponse>()
                 .ForMember(dest => dest.HolidayName, opt => opt.MapFrom(src => src.HolidayName.Value))
                  .ForMember(dest => dest.HolidayType, opt => opt.MapFrom(src => src.HolidayName.HolidayType.Value));
            CreateMap<MasterHolidayType, MasterDataTypeResponse>();
            CreateMap<MasterHolidayName, MasterHolidayNameResponse>()
                 .ForMember(dest => dest.HolidayType, opt => opt.MapFrom(src => src.HolidayType.Value));
            CreateMap<PagingResponse<MasterHoliday>, PagingResponse<MasterHolidayResponse>>();
            CreateMap<PagingResponse<MasterHolidayType>, PagingResponse<MasterDataTypeResponse>>();
            CreateMap<PagingResponse<MasterHolidayName>, PagingResponse<MasterHolidayNameResponse>>();
            #endregion

            #region Product
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductType, MasterDataResponse>();
            CreateMap<PagingResponse<ProductType>, PagingResponse<MasterDataResponse>>();
            CreateMap<MasterDataRequest, ProductType>();
            CreateMap<PagingResponse<Product>, PagingResponse<ProductResponse>>();
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>()
                 .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.Value))
                 .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.FabricWidth.Value))
                 .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Value));
            #endregion

            CreateMap<PurchaseEntryDetailResponse, ProductStock>()
                .ForMember(dest => dest.AvailableQty, opt => opt.MapFrom(src => src.Qty))
             .ForMember(dest => dest.AvailablePiece, opt => opt.MapFrom(src => src.Qty*1440));
            CreateMap<PurchaseEntryDetail, ProductStock>()
              .ForMember(dest => dest.AvailableQty, opt => opt.MapFrom(src => src.Qty))
              .ForMember(dest => dest.AvailablePiece, opt => opt.MapFrom(src => src.Qty*1440));
            CreateMap<PurchaseEntryDetailRequest, ProductStock>()
             .ForMember(dest => dest.AvailableQty, opt => opt.MapFrom(src => src.Qty))
             .ForMember(dest => dest.AvailablePiece, opt => opt.MapFrom(src => src.Qty*1440));

            #region Expense
            CreateMap<ExpenseNameRequest, ExpenseName>();
            CreateMap<ExpenseName, ExpenseNameResponse>()
                  .ForMember(dest => dest.ExpenseType, opt => opt.MapFrom(src => src.ExpenseType.Value));
            CreateMap<PagingResponse<ExpenseName>, PagingResponse<ExpenseNameResponse>>();

            CreateMap<MasterDataTypeRequest, ExpenseType>();
            CreateMap<ExpenseType, MasterDataTypeResponse>();
            CreateMap<PagingResponse<ExpenseType>, PagingResponse<MasterDataTypeResponse>>();

            CreateMap<ExpenseRequest, Expense>();
            CreateMap<Expense, ExpenseResponse>()
                  .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Employee.MasterJobTitle.Value))
                   .ForMember(dest => dest.ExpenseType, opt => opt.MapFrom(src => src.ExpenseName.ExpenseType.Value))
                   .ForMember(dest => dest.ExpenseTypeId, opt => opt.MapFrom(src => src.ExpenseName.ExpenseType.Id))
                    .ForMember(dest => dest.ExpenseShopCompany, opt => opt.MapFrom(src => src.ExpenseShopCompany.CompanyName))
                     .ForMember(dest => dest.ExpenseName, opt => opt.MapFrom(src => src.ExpenseName.Value))
                     .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
                     .ForMember(dest => dest.EnteredEmployeeName, opt => opt.MapFrom(src => src.EmployeeName));
            CreateMap<PagingResponse<Expense>, PagingResponse<ExpenseResponse>>();

            CreateMap<ExpenseShopCompanyRequest, ExpenseShopCompany>();
            CreateMap<ExpenseShopCompany, ExpenseShopCompanyResponse>();
            CreateMap<PagingResponse<ExpenseShopCompany>, PagingResponse<ExpenseShopCompanyResponse>>();
            #endregion

            #region Rent
            CreateMap<RentLocationRequest, RentLocation>();
            CreateMap<RentLocation, RentLocationResponse>();
            CreateMap<PagingResponse<RentLocation>, PagingResponse<RentLocationResponse>>();
            CreateMap<RentDetailRequest, RentDetail>();
            CreateMap<RentDetail, RentDetailResponse>()
                 .ForMember(dest => dest.RentLocation, opt => opt.MapFrom(src => src.RentLocation.LocationName));
            CreateMap<PagingResponse<RentDetail>, PagingResponse<RentDetailResponse>>();
            CreateMap<RentTransation, RentTransactionResponse>()
                .ForMember(dest => dest.RentLocation, opt => opt.MapFrom(src => src.RentDetail.RentLocation.LocationName))
                   .ForMember(dest => dest.PaidBy, opt => opt.MapFrom(src => src.PaidByEmp.FirstName+""+src.PaidByEmp.LastName)); ;
            CreateMap<PagingResponse<RentTransation>, PagingResponse<RentTransactionResponse>>();

            #endregion

            #region Stock
            CreateMap<OrderUsedCrystal, OrderUsedCrystalResponse>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.ProductStock.Product.ProductName))
                 .ForMember(dest => dest.Shape, opt => opt.MapFrom(src => src.ProductStock.Product.FabricWidth.Value))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.ProductStock.Product.Size.Value));
            CreateMap<OrderUsedCrystalRequest,OrderUsedCrystal>();
            #endregion

            #region Report
            CreateMap<CustomerAccountStatement, BillingTaxReportResponse>();
            #endregion

            #region Work Decription
            CreateMap<MasterDataTypeRequest, MasterWorkDescription>();
            CreateMap<MasterWorkDescription, MasterDataTypeResponse>();
            CreateMap<PagingResponse<MasterWorkDescription>, PagingResponse<MasterDataTypeResponse>>();
            CreateMap<OrderWorkDescriptionRequest, OrderWorkDescription>();
            CreateMap<OrderWorkDescription,OrderWorkDescriptionResponse>();
            #endregion

            #region Crystal
            CreateMap<MasterCrystalRequest, MasterCrystal>();
            CreateMap<MasterCrystal, MasterCrystalResponse>()
                  .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Value))
                 .ForMember(dest => dest.Shape, opt => opt.MapFrom(src => src.Shape.Value))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.Value));
            CreateMap<PagingResponse<MasterCrystal>, PagingResponse<MasterCrystalResponse>>();
            CreateMap<CrystalPurchaseRequest, CrystalPurchase>();
            CreateMap<CrystalPurchase,CrystalPurchaseResponse>()
                 .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.CompanyName))
                   .ForMember(dest => dest.SupplierContact, opt => opt.MapFrom(src => src.Supplier.Contact));
            CreateMap<CrystalPurchaseDetailRequest, CrystalPurchaseDetail>();
            CreateMap<CrystalPurchaseDetail, CrystalPurchaseDetailResponse>();
            CreateMap<PagingResponse<CrystalPurchase>, PagingResponse<CrystalPurchaseResponse>>();
            CreateMap<CrystalPurchaseDetail, CrystalPurchaseDetailResponse>()
                .ForMember(dest => dest.CrystalName, opt => opt.MapFrom(src => src.MasterCrystal.Name))
                .ForMember(dest => dest.CrystalShape, opt => opt.MapFrom(src => src.MasterCrystal.Shape.Value))
                .ForMember(dest => dest.CrystalSize, opt => opt.MapFrom(src => src.MasterCrystal.Size.Value))
                .ForMember(dest => dest.CrystalBrand, opt => opt.MapFrom(src => src.MasterCrystal.Brand.Value));
            CreateMap<CrystalPurchaseDetail, CrystalStock>()
                .ForMember(dest => dest.InStock, opt => opt.MapFrom(src => src.Qty))
                 .ForMember(dest => dest.InStockPieces, opt => opt.MapFrom(src => src.TotalPiece))
                  .ForMember(dest => dest.BalanceStock, opt => opt.MapFrom(src => src.Qty))
                   .ForMember(dest => dest.BalanceStockPieces, opt => opt.MapFrom(src => src.TotalPiece));

            CreateMap<CrystalStockRequest, CrystalStock>();
            CreateMap<CrystalStock, CrystalStockResponse>()
                  .ForMember(dest => dest.CrystalName, opt => opt.MapFrom(src => src.MasterCrystal.Name))
                   .ForMember(dest => dest.QtyPerPacket, opt => opt.MapFrom(src => src.MasterCrystal.QtyPerPacket))
                .ForMember(dest => dest.CrystalShape, opt => opt.MapFrom(src => src.MasterCrystal.Shape.Value))
                .ForMember(dest => dest.CrystalSize, opt => opt.MapFrom(src => src.MasterCrystal.Size.Value))
                 .ForMember(dest => dest.AlertQty, opt => opt.MapFrom(src => src.MasterCrystal.AlertQty))
                .ForMember(dest => dest.CrystalBrand, opt => opt.MapFrom(src => src.MasterCrystal.Brand.Value));

            CreateMap<CrystalStock, CrystalStockResponseExt>()
                .ForMember(dest => dest.CrystalName, opt => opt.MapFrom(src => src.MasterCrystal.Name))
                 .ForMember(dest => dest.QtyPerPacket, opt => opt.MapFrom(src => src.MasterCrystal.QtyPerPacket))
              .ForMember(dest => dest.CrystalShape, opt => opt.MapFrom(src => src.MasterCrystal.Shape.Value))
              .ForMember(dest => dest.CrystalSize, opt => opt.MapFrom(src => src.MasterCrystal.Size.Value))
               .ForMember(dest => dest.AlertQty, opt => opt.MapFrom(src => src.MasterCrystal.AlertQty))
              .ForMember(dest => dest.CrystalBrand, opt => opt.MapFrom(src => src.MasterCrystal.Brand.Value));

            CreateMap<PagingResponse<CrystalStock>, PagingResponse<CrystalStockResponse>>();

            CreateMap<CrystalTrackingOutRequest,CrystalTrackingOut>();
            CreateMap<CrystalTrackingOut,CrystalTrackingOutResponse>()
                .ForMember(dest => dest.OrderNo, opt => opt.MapFrom(src => src.OrderDetail.Order.OrderNo))
                .ForMember(dest => dest.KandooraNo, opt => opt.MapFrom(src => src.OrderDetail.OrderNo))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"));

            CreateMap<PagingResponse<CrystalTrackingOut>, PagingResponse<CrystalTrackingOutResponse>>();

            CreateMap<CrystalTrackingOutDetailRequest, CrystalTrackingOutDetail>();
            CreateMap<CrystalTrackingOutDetail, CrystalTrackingOutDetailResponse>()
                .ForMember(dest => dest.CrystalName, opt => opt.MapFrom(src => src.Crystal.Name))
             .ForMember(dest => dest.CrystalBrand, opt => opt.MapFrom(src => src.Crystal.Brand.Value))
             .ForMember(dest => dest.CrystalSize, opt => opt.MapFrom(src => src.Crystal.Size.Value))
             .ForMember(dest => dest.CrystalShape, opt => opt.MapFrom(src => src.Crystal.Shape.Value));


            CreateMap<CrystalTrackingOutDetail, CrystalStock>()
                .ForMember(dest => dest.OutStock, opt => opt.MapFrom(src => src.ReleasePacketQty))
                .ForMember(dest => dest.OutStockPieces, opt => opt.MapFrom(src => src.ReleasePieceQty+src.LoosePieces));
            #endregion

            #region Menu
            CreateMap<MasterMenu, MenuResponse>();
            #endregion

            #region Master Access
            CreateMap<MasterAccessRequest, MasterAccess>()
             .ForMember(dest => dest.MasterAccessDetails, opt => opt.MapFrom(src => src.MasterAccessDetails));
            CreateMap<MasterAccessDetailRequest, MasterAccessDetail>();
            CreateMap<MasterAccess, MasterAccessResponse>()
                   .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName+" "+src.Employee.LastName))
                   .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.UserRole.Name))
                   .ForMember(dest => dest.MenuName, opt => opt.MapFrom(src =>string.Join(", ", src.MasterAccessDetails.Select(x=>x.MasterMenu.Name).ToList())));

            CreateMap<MasterAccessDetail, MasterAccessDetailResponse>()
                 .ForMember(dest => dest.MenuName, opt => opt.MapFrom(src => src.MasterMenu.Name))
                   .ForMember(dest => dest.ParentMenuId, opt => opt.MapFrom(src => src.MasterMenu.ParentId))
                  .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.MasterMenu.Url));

            CreateMap<PagingResponse<MasterAccess>, PagingResponse<MasterAccessResponse>>();
            #endregion
        }
    }
}