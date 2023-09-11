using Microsoft.AspNetCore.Http;
using MKExpress.API.Enums;
using System.Collections.Generic;
using System.IO;

namespace MKExpress.API.Constants
{

    public static class StaticValues
    {
        #region User Role

        public static readonly Dictionary<string, UserRoleEnum> UserTypeRoleMap = new Dictionary<string, UserRoleEnum>()
    {
        {"1",UserRoleEnum.Admin},
        {"2",UserRoleEnum.User}
    };

        #endregion

        #region Error Messages
        public const string DataNotFoundError = "DataNotFound!";
        public const string DataNotFoundMessage = "Data does not exist!";
        public const string RecordAlreadyExistError = "RecordAlreadyExist!";
        public const string MasterDataTypeAlreadyExistError = "MasterDataTypeAlreadyExist!";
        public const string MasterDataTypeAlreadyExistMessage = "Master data type is already exist!";
        public const string InvalidMasterDataTypeError = "InvalidMasterDataTypeError!";
        public const string InvalidMasterDataTypeMessage = "Master data type is invalid!";
        public const string InvalidModuleNameError = "InvalidModuleName!";
        public const string InvalidModuleNameMessage = "Module name is invalid!";
        public const string ContactRequired = "Contact number is required!";
        public const string FirstNameRequired = "Firstname is required!";
        public const string PhotoNotSelectedError = "PhotoNotSelected!";
        public const string PhotoNotSelectedMessage = "Photo not selected or uploaded!";
        public const string ActiveOrderDeleteError = "ActiveOrderDeleteError!";
        public const string ActiveOrderDeleteMessage = "Only active order can be deleted!";
        public const string OrderAlreadyCancelledError = "OrderAlreadyCancelledError!";
        public const string OrderAlreadyCancelledMessage = "Order is already cancelled!";
        public const string OrderAlreadyDeliveredError = "OrderAlreadyDeliverdError!";
        public const string OrderAlreadyDeliverdMessage = "Order is already deliverd!";
        public const string OrderInProcessingError = "OrderInProcessingError!";
        public const string OrderInProcessingMessage = "You can't delete order because its inn processing status!";
        public const string OrderNotInActiveStateError = "OrderNotInActiveStateError!";
        public const string OrderNotInActiveStateMessage = "You can edit only active status order!";
        public const string OrderNotFoundError = "OrderNotFoundError!";
        public const string OrderNotFoundMessage = "Order does not found!";
        public const string OrderDeliveryDateError = "OrderDeliveryDateError!";
        public const string OrderDeliveryDateMessage = "Order Date can not be greater than Delivery Date!";
        public const string OrderDateChangeError = "OrderDateChangeError!";
        public const string OrderDateChangeMessage = "You can't change the order date because its status is not Active!";
        public const string OrderIsProceesingError = "OrderIsProceesingError!";
        public const string OrderIsProceesingMessage = "Order is in processing! you can not cancel or delete this order!";
        public const string OrderNoAlreadyExistError = "OrderNoAlreadyExistError!";
        public const string OrderNoAlreadyExistMessage = "Order no is already exist. please refresh the page and try again!";
        public const string OrderDetailError = "OrderDetailError!";
        public const string OrderDetailMessage = "Order details are invalid!";
        public const string OrderDateInFutureError = "OrderDateInFutureError!";
        public const string OrderDateInFutureMessage = "Order date can't be in future!";
        public const string OrderNotCompletedError = "OrderNotCompletedError!";
        public const string OrderNotCompletedMessage = "Some Kandoora's work type is not completed. so you cant delivered all quantities!";
        public const string KandooraNotCompletedError = "KandooraNotCompletedError!";
        public const string KandooraNotCompletedMessage = "Selected Kandoora's work type is not completed. so you cant delivered this quantity!";
        public const string OrderPartiallyCancelledError = "OrderPartiallyCancelledError!";
        public const string OrderPartiallyCancelledMessage = "Order is partially cancelled!";
        public const string RecordAlreadyDeletedError = "RecordAlreadyDeletedError!";
        public const string RecordAlreadyDeletedMessage = "Record is already deleted!";
        public const string CustomerAlreadyExistError = "CustomerAlreadyExistError!";
        public const string DataAlreadyExistError = "DataAlreadyExistError!";
        public const string DataAlreadyExistMessage = "Data Already Exist!";
        public const string InvalidDataError = "InvalidDataError!";
        public const string InvalidDataMessage = "You have sent invalid data in request!";
        public const string CustomerAlreadyExistMessage = "Customer is already exist with same name and contact number!";
        public const string KandooraExpenseHeadAlreadyExistError = "KandooraExpenseHeadAlreadyExistError!";
        public const string KandooraExpenseHeadAlreadyExistMessage = "Kandoora Expense Head Already Exist!";
        public const string InvalidInstallmentStartDate = "Please enter valid installment start date!";
        public const string OrderDetailNotFoundError = "OrderDetailNotFoundError!";
        public const string OrderDetailNotFoundMessage = "Order Detail(s) Not Found!";
        public const string EMIStartMonthYearError = "EMIStartMonthYearError!";
        public const string EMIStartMonthYearMessage = "EMI should be start from next month or later!";
        public const string HolidayTypeAlreadyExistError = "HolidayTypeExistError!";
        public const string HolidayTypeAlreadyExistMessage = "Holiday type is already exist!";
        public const string HolidayNameAlreadyExistError = "HolidayNameExistError!";
        public const string AddRecordError = "AddRecordError!";
        public const string AddRecordErrorMessage = "Unable add record error!";
        public const string RentUpdateDeleteError = "RentUpdateDeleteError!";
        public const string RentUpdateDeleteErrorMessage = "You can't update/delete this rent details. because you have already paid some rent!";
        public const string HolidayNameAlreadyExistMessage = "Holiday name is already exist!";
        public const string HolidayAlreadyExistError = "HolidayExistError!";
        public const string HolidayAlreadyExistMessage = "Holiday is already exist with same year!";
        public const string ExpenseTypeAlreadyExistError = "ExpenseTypeExistError!";
        public const string ExpenseTypeAlreadyExistMessage = "Expense type is already exist!";

        public const string RentLocationAlreadyExistError = "RentLocationExistError!";
        public const string RentLocationAlreadyExistMessage = "Rent location is already exist!";

        public const string RentAlreadyPaidError = "RentAlreadyPaidError!";
        public const string RentAlreadyPaidErrorMessage = "Rent is already paid!";

        public const string ExpenseCompanyAlreadyExistError = "ExpenseComapanyExistError!";
        public const string ExpenseCompanyAlreadyExistMessage = "Expense company is already exist!";

        public const string ExpenseNoAlreadyExistError = "ExpenseNoExistError!";
        public const string ExpenseNoAlreadyExistMessage = "Expense number is already exist!";

        public const string ExpenseNameAlreadyExistError = "ExpenseNameExistError!";
        public const string ExpenseNameAlreadyExistMessage = "Expense name is already exist!";
        public const string ChequeDateIsRequired = "Cheque date is required!";
        public const string ChequeNumberIsRequired = "Cheque number is required!";
        public const string ExpenseNameNotFoundError = "ExpenseNameNotFountError!";
        public const string ExpenseNameNotFoundMessage= "Expense name id not found - ";

        public const string CompanyNameAlreadyExistError = "CompanyShopNameExistError!";
        public const string CompanyNameAlreadyExistMessage = "Company/Shop name is already exist!";
        public const string DuplicateWorkTypeError = "DuplicateWorkTypeError!";
        public const string DuplicateWorkTypeMessage = "You have entered duplicate work type!";
        public const string InvalidWorkTypeError = "InvalidWorkTypeError!";
        public const string InvalidWorkTypeMessage = "You have entered invalid work type!";
        public const string WorkTypeNotEnteredError = "WorkTypeNotEnteredError!";
        public const string WorkTypeNotEnteredMessage = "You have not entered work type!";
        public const string WorkTypeUpdateError = "WorkTypeUpdateError!";
        public const string WorkTypeUpdateMessage = "You can update worktype of active or processing status kandoora only!";
        public const string RentExpenseNameIdNotFound = "Rent expense name id not found!";

        public const string DataIsInUseDeleteError = "DataIsInUseDeleteError!";
        public const string DataIsInUseDeleteMessage = "You can't delete this record because its already in use!";

        public const string FutureDateError = "FutureDateNotAllowed!";
        public const string FutureDateMessage = "Future date not allowed!";

        public const string InvalidCrystalPurchaseDataError = "InvalidCrystalPurchaseDataError!";
        public const string InvalidCrystalPurchaseDataMessage = "You have supplied some invalid data!";

        public const string SalaryAlreadyPaidError = "SalaryAlreadyPaidError!";
        public const string SalaryAlreadyPaidMessage = "Salary is already paid to the employee/staff!";
        public const string CrystalTrackingDetailNotFoundError = "CrystalTrackingDetailNotFoundError!";
        public const string CrystalTrackingDetailNotFoundMessage = "Crystal tracking details not found/supplied!";
        public const string CrystalTrackingAlreadyExistError = "CrystalTrackingAlreadyExistError!";
        public const string CrystalTrackingAlreadyExistMessage = "Crystal tracking details already exist for this kandoora!";
        public static string ErrorFileSize(long limitInBytes)
        {
            return $"Please upload a file with size less than {limitInBytes} bytes";
        }
        public static string ErrorInvalidContentType(string[] acceptableFormats)
        {
            return $"Please upload a valid file. Supported content types are: {string.Join(",", acceptableFormats)}";
        }
        public static string RecordAlreadyExistMessage(string recordType)
        {
            return $"Record already exist with same: {recordType}";
        }
        public static string GetCustomerProfilePhotoFileName(IFormFile profilePhoto, int customerId)
        {
            //creates a folder with expert id as the name and adds Profile_Photo inside it
            return $"{customerId}/Profile_Photo{Path.GetExtension(profilePhoto.FileName)}";
        }

        #region User Error
        public const string InvalidEmail = "Email address is invalid!";
        public const string InvalidStringLength = "Invalid input lengt!h";
        public const string UserWithSameEmailExists = "User with same email is alredy exists!";
        public const string ErrorUserRegister = "ErrorUserRegister";
        public const string RegistrationFailed = "RegistrationFailed";
        public const string ErrorIncorrectCredentials = "Wrong username or password!";
        public const string ErrorUserDoesNotExists = "User doesn't exist!";
        public const string ErrorChangePassword = "Unable to change the password!";
        public const string ChangedPasswordFailed = "ChangedPasswordFailed";
        public const string UserDoesNotExist = "UserDoesNotExist";
        public const string ErrorResetToken = "Reset token failure!";
        #endregion

        #region Customer Error
        public const string CustomerNotFoundError = "CustomerNotFound";
        public const string CustomerNotFoundMessage = "Customer does not exists!";
        #endregion

        #region Employee Error
        public const string EmployeeNotFoundError = "EmployeeNotFound";
        public const string JobTitleNotFoundError = "JobTitleNotFoundError";
        public const string ExpertiesNotFoundError = "ExpertiesNotFoundError";
        public const string EmployeeNotFoundMessage = "Employee does not exists!";
        public const string JobTitleNotFoundMessage = "Job title does not exists!";
        public const string ExpertiesNotFoundMessage = "Experties does not exists!";
        public const string InvalidJobTitleId = "Job title id is invalid!";
        public const string InvalidExpertiesId = "Experties id is invalid!";

        #endregion

        #endregion

        #region Constant Values

        public const string EmailClaim = "email";
        public const string UserIdClaim = "userId";
        public const string UserFirstnameClaim = "firstname";
        public const string UserLastnameClaim = "lastname";
        public const string UserRoleClaim = "role";
        public const string UserFullnameClaim = "fullname";
        public const string UserUsernameClaim = "username";
        public const string UserTypeClaim = "userType";
        public const string EmailHeader = "x-user-id";
        public const long MaxAllowedFileSize = 5 * 1024 * 1024;
        public const string WorkTypeStatusDone = "Completed";
        public const string WorkTypeStatusNotDone = "Not Started";
        public const string WorkTypeCode = "work_type";
        public const string TextAll = "ALL";
        public const string TextAllLower = "all";
        public const string TextAdvanceLower = "advance";
        public const string TextDeliveryLower = "delivery";
        public const string TextZeroInt = "0";
        public const string TextOneInt = "1";
        public const string TextTwoInt = "2";
        public const string TextThreeInt = "3";
        public const string TextFourInt = "4";
        public const string TextFiveInt = "5";
        public const string TextSixInt = "6";
        public const string TextSevenInt = "7";
        public const string TextNoPayment = "No Payment";

        #endregion

        #region API Path

        private const string ApiRoutePrefix = "api/";
        public const string SignUpUserPath = ApiRoutePrefix + "user/signup";
        public const string DeleteUserPath = ApiRoutePrefix + "user/delete";
        public const string LoginPath = ApiRoutePrefix + "user/token/login";
        public const string RefreshTokenPath = ApiRoutePrefix + "user/token/refresh";
        public const string UsersResetPasswordTokenPath = ApiRoutePrefix + "user/reset";
        public const string LogoutUsersPath = ApiRoutePrefix + "user/logout";
        public const string UsersPasswordPath = ApiRoutePrefix + "user/password";
        public const string UserRolePath = ApiRoutePrefix + "user/role";
        public const string UserPath = ApiRoutePrefix + "users";
        public const string CustomerPath = ApiRoutePrefix + "customers";
        public const string StockGetCrystalPath = ApiRoutePrefix + "stock/get/crystal";
        public const string StockGetOrderUsedCrystalPath = ApiRoutePrefix + "stock/get/order-used-crystal";
        public const string StockSaveOrderUsedCrystalPath = ApiRoutePrefix + "stock/save/order-used-crystal";

        public const string WorkDescriptionPath = ApiRoutePrefix + "work-description";
        public const string WorkDescriptionDeletePath = ApiRoutePrefix + "work-description/{id}";
        public const string WorkDescriptionByIdPath = ApiRoutePrefix + "work-description/get/{id}";
        public const string WorkDescriptionByWorkTypePath = ApiRoutePrefix + "work-description/get/work-type";
        public const string WorkDescriptionSearchPath = ApiRoutePrefix + "work-description/search";
        public const string WorkDescriptionSaveOrderPath = ApiRoutePrefix + "work-description/order/save";
        public const string WorkDescriptionGetOrderPath = ApiRoutePrefix + "work-description/order/get";

        public const string UserPermissionResourcePath = ApiRoutePrefix + "permission/resource";
        public const string UserPermissionResourceTypePath = ApiRoutePrefix + "permission/resource-type";
        public const string UserPermissionSearchPath = ApiRoutePrefix + "permission/search";
        public const string UserPermissionSetDefaultPath = ApiRoutePrefix + "permission/set-default";
        public const string UserPermissionPath = ApiRoutePrefix + "permission";
        public const string UserPermissionByRoleNamePath = ApiRoutePrefix + "permission/role/{roleName}";
        public const string UserPermissionGetRolesPath = ApiRoutePrefix + "permission/roles";
        public const string DashboardPath = ApiRoutePrefix + "dashboard";
        public const string DashboardGetWeeklySalePath = ApiRoutePrefix + "dashboard/get/weekly-sales";
        public const string DashboardGetDailySalePath = ApiRoutePrefix + "dashboard/get/daily-sales";
        public const string DashboardGetMonthlySalePath = ApiRoutePrefix + "dashboard/get/monthly-sales";
        public const string DashboardGetEmployeePath = ApiRoutePrefix + "dashboard/get/employee";
        public const string DashboardGetOrderPath = ApiRoutePrefix + "dashboard/get/order";
        public const string DashboardGetExpensePath = ApiRoutePrefix + "dashboard/get/expense";
        public const string DashboardGetCrystalPath = ApiRoutePrefix + "dashboard/get/crystal";
        public const string CustomerDeletePath = ApiRoutePrefix + "customers/{id}";
        public const string CustomerByIdPath = ApiRoutePrefix + "customers/get/{id}";
        public const string CustomerByContactNoPath = ApiRoutePrefix + "customers/get/by-contact";
        public const string CustomerGetPreAmountStatementPath = ApiRoutePrefix + "customers/get/statement";
        public const string CustomerAddAdvanceAmountPath = ApiRoutePrefix + "customers/add/advance-amount";
        public const string CustomerSearchPath = ApiRoutePrefix + "customers/search";
        public const string HolidayPath = ApiRoutePrefix + "holidays";
        public const string HolidayDeletePath = ApiRoutePrefix + "holidays/{id}";
        public const string HolidayByIdPath = ApiRoutePrefix + "holidays/get/{id}";
        public const string HolidayIsHolidayPath = ApiRoutePrefix + "holidays/get/is-holiday";
        public const string HolidayGetByDatePath = ApiRoutePrefix + "holidays/get/by-date";
        public const string HolidayGetByMonthYearPath = ApiRoutePrefix + "holidays/get/by-month-year/{month}/{year}";
        public const string HolidaySearchPath = ApiRoutePrefix + "holidays/search";

        public const string HolidayTypePath = ApiRoutePrefix + "holidays/type";
        public const string HolidayTypeDeletePath = ApiRoutePrefix + "holidays/type/{id}";
        public const string HolidayTypeByIdPath = ApiRoutePrefix + "holidays/type/get/{id}";
        public const string HolidayTypeSearchPath = ApiRoutePrefix + "holidays/type/search";

        public const string HolidayNamePath = ApiRoutePrefix + "holidays/name";
        public const string HolidayNameDeletePath = ApiRoutePrefix + "holidays/name/{id}";
        public const string HolidayNameByIdPath = ApiRoutePrefix + "holidays/name/get/{id}";
        public const string HolidayNameSearchPath = ApiRoutePrefix + "holidays/name/search";

        public const string ExpensePath = ApiRoutePrefix + "expense";
        public const string ExpenseDeletePath = ApiRoutePrefix + "expense/{id}";
        public const string ExpenseByIdPath = ApiRoutePrefix + "expense/get/{id}";
        public const string ExpenseGetNumberPath = ApiRoutePrefix + "expense/get/expense-no";
        public const string GetHeadWiseExpenseSum = ApiRoutePrefix + "expense/get/head-wise-sum";

        public const string ExpenseSearchPath = ApiRoutePrefix + "expense/search";

        public const string ExpenseTypePath = ApiRoutePrefix + "expense/type";
        public const string ExpenseTypeDeletePath = ApiRoutePrefix + "expense/type/{id}";
        public const string ExpenseTypeByIdPath = ApiRoutePrefix + "expense/type/get/{id}";
        public const string ExpenseTypeSearchPath = ApiRoutePrefix + "expense/type/search";

        public const string ExpenseNamePath = ApiRoutePrefix + "expense/name";
        public const string ExpenseNameDeletePath = ApiRoutePrefix + "expense/name/{id}";
        public const string ExpenseNameByIdPath = ApiRoutePrefix + "expense/name/get/{id}";
        public const string ExpenseNameSearchPath = ApiRoutePrefix + "expense/name/search";

        public const string ExpenseCompanyPath = ApiRoutePrefix + "expense/company";
        public const string ExpenseCompanyDeletePath = ApiRoutePrefix + "expense/company/{id}";
        public const string ExpenseCompanyByIdPath = ApiRoutePrefix + "expense/company/get/{id}";
        public const string ExpenseCompanySearchPath = ApiRoutePrefix + "expense/company/search";

        public const string AccountGetSummaryReportPath = ApiRoutePrefix + "account/get/summary-report";
        public const string EmployeePath = ApiRoutePrefix + "employees";
        public const string EmployeeDeletePath = ApiRoutePrefix + "employees/{id}";
        public const string EmployeeByIdPath = ApiRoutePrefix + "employees/get/{id}";
        public const string EmployeeSearchPath = ApiRoutePrefix + "employees/search";
        public const string EmployeeSearchAllPath = ApiRoutePrefix + "employees/search/all";
        public const string EmployeeByJobIdPath = ApiRoutePrefix + "employees/get/by-job-id";
        public const string EmployeeAllActiveDeactivePath = ApiRoutePrefix + "employees/get/active-emp";
        public const string EmployeeActiveDeactivePath = ApiRoutePrefix + "employees/update/active-emp/{empId}/{isActive}";
        public const string EmployeePaySalaryPath = ApiRoutePrefix + "employees/update/pay-salary/{paidOn}/{id}/{salary}";
        public const string EmployeeGetSalarySlipPath = ApiRoutePrefix + "employees/get/salary-slip";
        public const string EmployeeGetSalaryLedgerPath = ApiRoutePrefix + "employees/get/salary-ledger";
        public const string EmployeeSendAlertPath = ApiRoutePrefix + "employees/send/alert/{empId}";
        public const string PurchaseEntryPath = ApiRoutePrefix + "purchase-entry";
        public const string PurchaseEntryDeletePath = ApiRoutePrefix + "purchase-entry/{id}";
        public const string PurchaseEntryByIdPath = ApiRoutePrefix + "purchase-entry/get/{id}";
        public const string PurchaseEntryGetPurchaseNoPath = ApiRoutePrefix + "purchase-entry/get/purchase-no";
        public const string PurchaseEntrySearchPath = ApiRoutePrefix + "purchase-entry/search";
        public const string OrderPath = ApiRoutePrefix + "orders";
        public const string OrderDeletePath = ApiRoutePrefix + "orders/{id}";
        public const string OrderByIdPath = ApiRoutePrefix + "orders/get/{id}";
        public const string OrderDetailByIdPath = ApiRoutePrefix + "orders/detail/get/{id}";
        public const string OrderGetOrderNoPath = ApiRoutePrefix + "orders/get/order-no";
        public const string OrderGetOrderNosPath = ApiRoutePrefix + "orders/get/order-nos";
        public const string OrderGetOrderDetailsPath = ApiRoutePrefix + "orders/get/order-details";
        public const string OrderGetOrderAlertsPath = ApiRoutePrefix + "orders/get/order-alerts";
        public const string OrderGetPreAmountPath = ApiRoutePrefix + "orders/get/previous-amount";
        public const string OrderSearchPath = ApiRoutePrefix + "orders/search";
        public const string OrderSearchCancelledOrdersPath = ApiRoutePrefix + "orders/search/cancelled-orders";
        public const string OrderSearchByCustomersPath = ApiRoutePrefix + "orders/search/by-customer";
        public const string OrderSearchBySalesmanPath = ApiRoutePrefix + "orders/search/by-salesman";
        public const string OrderSearchBySalesmanAndDateRangePath = ApiRoutePrefix + "orders/search/by-salesman/{fromDate}/{toDate}";
        public const string OrderSearchDeletedOrdersPath = ApiRoutePrefix + "orders/search/deleted-orders";
        public const string OrderSearchPendingOrdersPath = ApiRoutePrefix + "orders/search/pending-orders";
        public const string OrderSearchWithFilterPath = ApiRoutePrefix + "orders/search/filter";
        public const string OrderDetailCancelPath = ApiRoutePrefix + "orders/cancel/order-detail";
        public const string OrderCancelPath = ApiRoutePrefix + "orders/cancel/order";
        public const string OrderGetMeasurementPath = ApiRoutePrefix + "orders/get/customer-measurement";
        public const string OrderGetMeasurementsPath = ApiRoutePrefix + "orders/get/customer-measurements";
        public const string OrderGetCancelledOrderPath = ApiRoutePrefix + "orders/get/cancelled-orders";
        public const string OrderGetPendingOrderPath = ApiRoutePrefix + "orders/get/pending-orders";
        public const string OrderGetDeletedOrderPath = ApiRoutePrefix + "orders/get/deleted-orders";
        public const string OrderGetOrderByDeliveryDatePath = ApiRoutePrefix + "orders/get/delivery-date/{fromDate}/{toDate}";
        public const string OrderSearchOrderByDeliveryDatePath = ApiRoutePrefix + "orders/search/delivery-date";
        public const string OrderGetOrderBySalesmanPath = ApiRoutePrefix + "orders/get/by-salesman";
        public const string OrderSearchAlertPath = ApiRoutePrefix + "orders/search/alert";
        public const string OrderGetOrderBySalesmanAndDateRangePath = ApiRoutePrefix + "orders/get/by-salesman/{fromDate}/{toDate}";
        public const string OrderGetOrderByCustomerPath = ApiRoutePrefix + "orders/get/by-customer";
        public const string OrdersWorkTypeStatusPath = ApiRoutePrefix + "orders/work-type-status";
        public const string OrdersUpdateMeasurementPath = ApiRoutePrefix + "orders/update/measurement";
        public const string OrdersUpdateModelNoPath = ApiRoutePrefix + "orders/update/model-no";
        public const string OrdersUpdateDeliveryPaymentPath = ApiRoutePrefix + "orders/update/delivery-payment";
        public const string OrdersEditPath = ApiRoutePrefix + "orders/update/edit";
        public const string OrdersUpdateStatementPath = ApiRoutePrefix + "orders/update/customer/statement";
        public const string OrdersUpdateDesignModelPath = ApiRoutePrefix + "orders/update/design-model/{orderDetailId}/{modelId}";
        public const string OrdersUpdateOrderDatePath = ApiRoutePrefix + "orders/update/order-date/{orderId}";
        public const string OrdersGetPaidAmountByCustomerPath = ApiRoutePrefix + "orders/get/customer/payment";
        public const string OrdersGetSampleCountInPreOrderPath = ApiRoutePrefix + "orders/get/sample/count";
        public const string OrdersGetOrderStatusListPath = ApiRoutePrefix + "orders/get/status/list";
        public const string OrdersGetOrderNoByContactPath = ApiRoutePrefix + "orders/get/order-no/contact";
        public const string OrdersGetModalNoByContactPath = ApiRoutePrefix + "orders/get/modal-no/contact";
        public const string OrdersGetAdvancePaymentStatementPath = ApiRoutePrefix + "orders/get/customer/payment/statement";
        public const string OrdersGetOrdersQtyPath = ApiRoutePrefix + "orders/get/order-qty";
        public const string OrderDetailsGetByWorkTypePath = ApiRoutePrefix + "orders/detail/get/by/work-type";
        public const string OrderDetailsSearchByWorkTypePath = ApiRoutePrefix + "orders/detail/search/by/work-type";
        public const string SupplierPath = ApiRoutePrefix + "suppliers";
        public const string SupplierDeletePath = ApiRoutePrefix + "suppliers/{id}";
        public const string SupplierByIdPath = ApiRoutePrefix + "suppliers/get/{id}";
        public const string SupplierSearchPath = ApiRoutePrefix + "suppliers/search";
        public const string DesignCategoryPath = ApiRoutePrefix + "design-category";
        public const string DesignCategoryDeletePath = ApiRoutePrefix + "design-category/{id}";
        public const string DesignCategoryByIdPath = ApiRoutePrefix + "design-category/get/{id}";
        public const string DesignCategorySearchPath = ApiRoutePrefix + "design-category/search";
        public const string ProductPath = ApiRoutePrefix + "product";
        public const string ProductDeletePath = ApiRoutePrefix + "product/{id}";
        public const string ProductByIdPath = ApiRoutePrefix + "product/get/{id}";
        public const string ProductGetInvoiceNoPath = ApiRoutePrefix + "product/get/invoice-no";
        public const string ProductSearchPath = ApiRoutePrefix + "product/search";

        public const string ProductTypePath = ApiRoutePrefix + "product-type";
        public const string ProductTypeDeletePath = ApiRoutePrefix + "product-type/{id}";
        public const string ProductTypeByIdPath = ApiRoutePrefix + "product-type/get/{id}";
        public const string ProductTypeSearchPath = ApiRoutePrefix + "product-type/search";

        public const string MasterDataPath = ApiRoutePrefix + "master-data";
        public const string MasterDataDeletePath = ApiRoutePrefix + "master-data/{id}";
        public const string MasterDataByIdPath = ApiRoutePrefix + "master-data/get/{id}";
        public const string MasterDataSearchPath = ApiRoutePrefix + "master-data/search";
        public const string MasterDataGetByTypePath = ApiRoutePrefix + "master-data/get/by-type";
        public const string MasterDataGetByTypesPath = ApiRoutePrefix + "master-data/get/by-types";
        public const string MasterDataTypePath = ApiRoutePrefix + "master-data-type";
        public const string MasterDataTypeDeletePath = ApiRoutePrefix + "master-data-type/{id}";
        public const string MasterDataTypeByIdPath = ApiRoutePrefix + "master-data-type/get/{id}";
        public const string MasterDataTypeSearchPath = ApiRoutePrefix + "master-data-type/search";
        public const string DesignSamplePath = ApiRoutePrefix + "design-sample";
        public const string DesignSampleDeletePath = ApiRoutePrefix + "design-sample/{id}";
        public const string DesignSampleByIdPath = ApiRoutePrefix + "design-sample/get/{id}";
        public const string DesignSampleByCategoryIdPath = ApiRoutePrefix + "design-sample/get/category/{categoryId}";
        public const string DesignSampleSearchPath = ApiRoutePrefix + "design-sample/search";
        public const string JobTitlePath = ApiRoutePrefix + "job-title";
        public const string JobTitleDeletePath = ApiRoutePrefix + "job-title/{id}";
        public const string JobTitleByIdPath = ApiRoutePrefix + "job-title/get/{id}";
        public const string JobTitleSearchPath = ApiRoutePrefix + "job-title/search";
        public const string MonthlyAttendencePath = ApiRoutePrefix + "monthly-attendence";
        public const string MonthlyDailyAttendencePath = ApiRoutePrefix + "daily-attendence";
        public const string MonthlyGetDailyAttendencePath = ApiRoutePrefix + "get/daily-attendence";
        public const string MonthlyAttendenceDeletePath = ApiRoutePrefix + "monthly-attendence/{id}";
        public const string MonthlyAttendenceByIdPath = ApiRoutePrefix + "monthly-attendence/get/{id}";
        public const string FileStoragePath = ApiRoutePrefix + "file-storage";
        public const string FileUploadPath = ApiRoutePrefix + "file-upload";
        public const string FileStorageDeletePath = ApiRoutePrefix + "file-storage/{id}";
        public const string FileStorageByIdPath = ApiRoutePrefix + "file-storage/get/{id}";
        public const string FileStorageByModuleNamePath = ApiRoutePrefix + "file-storage/module-name/{moduleName}";
        public const string FileStorageByModuleIdsPath = ApiRoutePrefix + "file-storage/module-ids/{moduleName}";
        public const string MonthlyAttendenceByEmpIdPath = ApiRoutePrefix + "monthly-attendence/get/emp/{employeeId}";
        public const string MonthlyAttendenceByEmpIdMonthYearPath = ApiRoutePrefix + "monthly-attendence/get/emp-month-year/{employeeId}/{month}/{year}";
        public const string MonthlyAttendenceByMonthYearPath = ApiRoutePrefix + "monthly-attendence/get/month-year/{month}/{year}";
        public const string MonthlyAttendenceSearchPath = ApiRoutePrefix + "monthly-attendence/search";
        public const string DropdownEmployeePath = ApiRoutePrefix + "dropdown/employees";
        public const string DropdownCustomerPath = ApiRoutePrefix + "dropdown/customers";
        public const string DropdownCustomerOrderPath = ApiRoutePrefix + "dropdown/customer-orders";
        public const string DropdownJobTitlePathPath = ApiRoutePrefix + "dropdown/job-titles";
        public const string DropdownProductPath = ApiRoutePrefix + "dropdown/products";
        public const string DropdownSupplierPath = ApiRoutePrefix + "dropdown/suppliers";
        public const string DropdownDesignCategoryPath = ApiRoutePrefix + "dropdown/design-category";
        public const string DropdownOrderDetailNosPath = ApiRoutePrefix + "dropdown/order-detail-nos";
        public const string DropdownWorkTypePath = ApiRoutePrefix + "dropdown/work-types";
        public const string WorkTypeStatusPath = ApiRoutePrefix + "work-type-status";
        public const string WorkTypeStatusUpdateExistingPath = ApiRoutePrefix + "work-type-status/update/existing";
        public const string WorkTypeStatusUpdateNotePath = ApiRoutePrefix + "work-type-status/update/existing/note/{orderDetailId}";
        public const string WorkTypeStatusByOrderIdPath = ApiRoutePrefix + "work-type-status/get/by/order-id";
        public const string WorkTypeSumAmountPath = ApiRoutePrefix + "work-type-status/get/sum-amount";

        public const string MasterDataKandooraExpenseHeadPath = ApiRoutePrefix + "master-data/kandoora/head";
        public const string MasterDataKandooraExpenseHeadDeletePath = ApiRoutePrefix + "master-data/kandoora/head/{id}";
        public const string MasterDataKandooraExpenseHeadByIdPath = ApiRoutePrefix + "master-data/kandoora/head/get/{id}";
        public const string MasterDataKandooraExpenseHeadSearchPath = ApiRoutePrefix + "master-data/kandoora/head/search";

        public const string MasterDataKandooraExpensePath = ApiRoutePrefix + "master-data/kandoora/expense";
        public const string MasterDataKandooraExpenseGetSumPath = ApiRoutePrefix + "master-data/kandoora/expense/get/sum";

        public const string EmployeeAdvancePaymentPath = ApiRoutePrefix + "employee-advance-payment";
        public const string EmployeeAdvancePaymentDeletePath = ApiRoutePrefix + "employee-advance-payment/{id}";
        public const string EmployeeAdvancePaymentByIdPath = ApiRoutePrefix + "employee-advance-payment/get/{id}";
        public const string EmployeeAdvancePaymentByEmployeeIdPath = ApiRoutePrefix + "employee-advance-payment/get/by-employee/{id}";
        public const string EmployeeAdvancePaymentSearchPath = ApiRoutePrefix + "employee-advance-payment/search";
        public const string EmployeeAdvancePaymentGetStatementPath = ApiRoutePrefix + "employee-advance-payment/statement/{empId}";

        public const string RentLocationPath = ApiRoutePrefix + "rent/location";
        public const string RentLocationDeletePath = ApiRoutePrefix + "rent/location/{id}";
        public const string RentLocationByIdPath = ApiRoutePrefix + "rent/location/get/{id}";
        public const string RentLocationSearchPath = ApiRoutePrefix + "rent/location/search";

        public const string RentDetailPath = ApiRoutePrefix + "rent/detail";
        public const string RentDetailDeletePath = ApiRoutePrefix + "rent/detail/{id}";
        public const string RentDetailByIdPath = ApiRoutePrefix + "rent/detail/get/{id}";
        public const string RentDetailSearchPath = ApiRoutePrefix + "rent/detail/search";
        public const string RentDetailTransactionPath = ApiRoutePrefix + "rent/detail/transaction";
        public const string RentGetDueRentPath = ApiRoutePrefix + "rent/detail/transaction/get/due-rent";
        public const string RentSearchDeuRentPath = ApiRoutePrefix + "rent/detail/transaction/search/deu-rent";
        public const string RentPayDeuRentPath = ApiRoutePrefix + "rent/detail/transaction/pay/deu-rent";

        public const string ReportWorkerPerformancePath = ApiRoutePrefix + "report/worker/performance";
        public const string ReportDailyStatusPath = ApiRoutePrefix + "report/order/daily-status-report";
        public const string ReportBillingTaxPath = ApiRoutePrefix + "report/order/bill-tax-report";
        public const string ReportEachKandooraExpenseReportPath = ApiRoutePrefix + "report/order/eack-kandoora-exp-report";
        public const string ReportSearchEachKandooraExpenseReportPath = ApiRoutePrefix + "report/order/search/eack-kandoora-exp-report";
        public const string ReportDailyWorkStatementReportPath = ApiRoutePrefix + "report/order/daily-work-statement-report";
        public const string ReportSearchDailyWorkStatementReportPath = ApiRoutePrefix + "report/order/search/daily-work-statement-report";
        public const string ReportBillingCancelTaxPath = ApiRoutePrefix + "report/order/bill-cancel-tax-report";
        public const string ReportPaymentSummaryPath = ApiRoutePrefix + "report/order/payment-summary";

        public const string CrystalMasterPath = ApiRoutePrefix + "crystal/master";
        public const string CrystalMasterDeletePath = ApiRoutePrefix + "crystal/master/{id}";
        public const string CrystalMasterByIdPath = ApiRoutePrefix + "crystal/master/get/{id}";
        public const string CrystalMasterSearchPath = ApiRoutePrefix + "crystal/master/search";
        public const string CrystalMasterGetCrystalIdPath = ApiRoutePrefix + "crystal/master/get/crystal-id";

        public const string CrystalTrackingOutPath = ApiRoutePrefix + "crystal/Track/out";
        public const string CrystalTrackingOutUpdateReturnPath = ApiRoutePrefix + "crystal/Track/out/update/return";
        public const string CrystalTrackingOutDeletePath = ApiRoutePrefix + "crystal/Track/out/{id}";
        public const string CrystalTrackingOutDetailDeletePath = ApiRoutePrefix + "crystal/Track/out/detail/{id}";
        public const string CrystalTrackingOutByIdPath = ApiRoutePrefix + "crystal/Track/out/get/{id}";
        public const string CrystalTrackingOutByOrderDetailIdPath = ApiRoutePrefix + "crystal/Track/out/get/order-detail-no/{id}";
        public const string CrystalTrackingOutGetOrderNoUsedInTrackingPath = ApiRoutePrefix + "crystal/Track/out/get/order-detail-no/{crystalId}/{releaseDate}";
        public const string CrystalTrackingOutGetRangeOrderNoUsedInTrackingPath = ApiRoutePrefix + "crystal/Track/out/get/order-detail-no/range/{crystalId}/{fromDate}/{toDate}";
        public const string CrystalTrackingOutConsumedDetailsPath = ApiRoutePrefix + "crystal/Track/out/get/consumed-details";
        public const string CrystalTrackingOutSearchPath = ApiRoutePrefix + "crystal/Track/out/search";
        public const string CrystalTrackingOutAddNotePath = ApiRoutePrefix + "crystal/Track/out/add/note";

        public const string CrystalPurchasePath = ApiRoutePrefix + "crystal/purchase";
        public const string CrystalPurchaseDeletePath = ApiRoutePrefix + "crystal/purchase/{id}";
        public const string CrystalPurchaseByIdPath = ApiRoutePrefix + "crystal/purchase/get/{id}";
        public const string CrystalPurchaseSearchPath = ApiRoutePrefix + "crystal/purchase/search";
        public const string CrystalPurchaseGetNumberPath = ApiRoutePrefix + "crystal/purchase/get/number";
        public const string CrystalStockGetAlertPath = ApiRoutePrefix + "crystal/stock/get/alert";
        public const string CrystalStockGetDetailPath = ApiRoutePrefix + "crystal/stock/get/details";
        public const string CrystalStockByIdPath = ApiRoutePrefix + "crystal/stock/get/{id}";
        public const string CrystalStockPath = ApiRoutePrefix + "crystal/stock"; 
        public const string CrystalStockSearchAlertPath = ApiRoutePrefix + "crystal/stock/search/alert";
        public const string CrystalStockSearchDetailPath = ApiRoutePrefix + "crystal/stock/search/detail";

        public const string HealthGetDatabaseNamePath = ApiRoutePrefix + "health/get/database/name";

        public const string MasterAccessPath = ApiRoutePrefix + "master/access";
        public const string MasterAccessGetByIdPath = ApiRoutePrefix + "master/access/get/{id}";
        public const string MasterAccessDeletePath = ApiRoutePrefix + "master/access/{id}";
        public const string MasterAccessSearchPath = ApiRoutePrefix + "master/access/search";
        public const string MasterAccessCheckUserNamePath = ApiRoutePrefix + "master/access/exist/username";
        public const string MasterAccessChanePasswordPath = ApiRoutePrefix + "master/access/change/password";
        public const string MasterAccessGetMenusPath = ApiRoutePrefix + "master/access/get/menu";
        public const string MasterAccessLoginPath = ApiRoutePrefix + "master/access/get/login";

        #endregion

        #region Resource Name

        private const string BaseResourcesPath = "MKExpress.API.Data.Resources";
        public const string RoleResourceName = BaseResourcesPath + ".Roles.csv";

        #endregion

        #region Message
        public const string EmpAdvanceExpenseDescription = "Employee/Staff Addvance Salary.";
        #endregion
    }
}