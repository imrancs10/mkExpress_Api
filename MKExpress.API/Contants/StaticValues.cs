namespace MKExpress.API.Contants
{
    public static class StaticValues
    {
        #region API Paths
        public const string APIPrefix = "v1/[controller]/";

        #region Login API
        public const string LoginPath = "user/login";
        public const string LoginUserRegisterPath = "user/register";
        public const string LoginUserChangePasswordPath = "user/change/password";
        public const string LoginUserResetPasswordPath = "user/reset/password";
        public const string LoginUserUpdateProfilePath = "user/update/profile";
        public const string LoginUserDeleteProfilePath = "user/delete/profile/{email}";
        public const string UserBlockPath = "user/block/{email}";
        public const string UserAssignRolePath = "user/update/{email}/{role}";
        public const string UserResetEmailVerifyCodePath = "user/update/verify/code/{email}";
        public const string LoginUserVerifyEmailPath = "user/verify/email/{token}";
        #endregion

        #region AppSetting API
        public const string AppSettingPath = APIPrefix;
        public const string AppSettingGroupPath = APIPrefix+"groups";
        public const string AppSettingByIdPath = APIPrefix + "get/{id}";
        public const string AppSettingSearchPath = APIPrefix + "search";
        public const string AppSettingDeletePath = APIPrefix + "{id}";
        #endregion

        #region Master Data API
        public const string MasterDataPath = APIPrefix+"master-data";
        public const string MasterDataDeletePath = APIPrefix + "master-data/{id}";
        public const string MasterDataByIdPath = APIPrefix + "master-data/get/{id}";
        public const string MasterDataSearchPath = APIPrefix + "master-data/search";
        public const string MasterDataGetByTypePath = APIPrefix + "master-data/get/by-type";
        public const string MasterDataGetByTypesPath = APIPrefix + "master-data/get/by-types";
        public const string MasterDataTypePath = APIPrefix + "master-data-type";
        public const string MasterDataTypeDeletePath = APIPrefix + "master-data-type/{id}";
        public const string MasterDataTypeByIdPath = APIPrefix + "master-data-type/get/{id}";
        public const string MasterDataTypeSearchPath = APIPrefix + "master-data-type/search";

        public const string MasterJourneyPath = APIPrefix + "master-journey";
        public const string MasterJourneyDeletePath = APIPrefix + "master-journey/{id}";
        public const string MasterJourneyByIdPath = APIPrefix + "master-journey/get/{id}";
        public const string MasterJourneySearchPath = APIPrefix + "master-journey/search";
        public const string MasterJourneyDropdownPath = APIPrefix + "master-journey/dropdown/journey";

        #region Master Roles
        public const string MasterRolePath = APIPrefix + "master-role";
        public const string MasterRoleDeletePath = APIPrefix + "master-role/{id}";
        public const string MasterRoleByIdPath = APIPrefix + "master-role/get/{id}";
        public const string MasterRoleSearchPath = APIPrefix + "master-role/search";
        #endregion

        #region Master Roles
        public const string MasterMenuPath = APIPrefix + "master-menu";
        public const string MasterMenuDeletePath = APIPrefix + "master-menu/{id}";
        public const string MasterMenuByIdPath = APIPrefix + "master-menu/get/{id}";
        public const string MasterMenuSearchPath = APIPrefix + "master-menu/search";
        #endregion

        #region Master Roles Menu Mapper
        public const string MasterRoleMenuMapperPath = APIPrefix + "master/menu/role/mapper/add";
        public const string MasterRoleMenuMapperGetAllPath = APIPrefix + "master/menu/role/mapper/get/all";
        public const string MasterRoleMenuMapperDeleteByRolePath = APIPrefix + "master/menu/role/mapper/delete/role/{id}";
        public const string MasterRoleMenuMapperDeleteByIdPath = APIPrefix + "master/menu/role/mapper/delete/{id}";
        public const string MasterRoleMenuMapperByIdPath = APIPrefix + "master/menu/role/mapper/get/{id}";
        public const string MasterRoleMenuMapperSearchPath = APIPrefix + "master/menu/role/mapper/search";
        #endregion

        #endregion

        #region Customer API Path
        public const string CustomerDeletePath = APIPrefix + "{id}";
        public const string CustomerByIdPath = APIPrefix + "get/{id}";
        public const string CustomerByContactNoPath = APIPrefix + "get/by-contact";
        public const string CustomerSearchPath = APIPrefix + "search";
        public const string CustomerGetDropdownPath = APIPrefix + "get/dropdown";
        public const string CustomerPath = APIPrefix;
        #endregion

        #region Dashboard API Path
        public const string DashboardShipmenyStatusCountPath = APIPrefix+"dashboard/status/count";
        public const string DashboardShipmenyStatusWiseCountPath = APIPrefix + "dashboard/status/wise/count";
        #endregion

        #region Assign Member
        public const string GetCourierRunsheetPath = APIPrefix + "get/courier/runsheet";
        #endregion

        #region Third Party API Path
        public const string ThirdPartyDeletePath = APIPrefix + "{id}";
        public const string ThirdPartyByIdPath = APIPrefix + "get/{id}";
        public const string ThirdPartyGetShipmentPath = APIPrefix + "get/shipment/{id}/{fromDate}/{toDate}";
        public const string ThirdPartyAddShipmentPath = APIPrefix + "add/shipments";
        public const string ThirdPartySearchPath = APIPrefix + "search";
        public const string ThirdPartyPath = APIPrefix;
        #endregion

        #region Container API Path
        public const string ContainerPath = APIPrefix;
        public const string ContainerGetByIdPath = APIPrefix + "get/{id}";
        public const string ContainerDeletePath = APIPrefix + "delete/{id}";
        public const string ContainerSearchPath = APIPrefix + "search";
        public const string ContainerGetJourneyPath = APIPrefix + "get/journey/{containerNo}";
        public const string ContainerClosePath = APIPrefix + "close/{containerId}";
        public const string ContainerAddShipmentPath = APIPrefix + "add/shipments/{containerId}/{shipmentNo}";
        public const string ContainerRemoveShipmentPath = APIPrefix + "remove/shipments/{containerId}/{shipmentNo}";
        public const string ContainerCheckInPath = APIPrefix + "in/{containerId}/{containerJourneyId}";
        public const string ContainerCheckOutPath = APIPrefix + "out/{containerId}/{containerJourneyId}";
        #endregion

        #region Logistic Region API Path
        public const string LogisticRegionDeletePath = APIPrefix + "{id}";
        public const string LogisticRegionByIdPath = APIPrefix + "get/{id}";
        public const string LogisticRegionSearchPath = APIPrefix + "search";
        public const string LogisticRegionPath = APIPrefix;
        #endregion

        #region Shipment
        public const string ShipmentPath = APIPrefix;
        public const string ShipmentByIdPath = APIPrefix + "get/{id}";
        public const string ShipmentTrackingByShipmentIdPath = APIPrefix + "get/tracking/{id}";
        public const string ShipmentByIdsPath = APIPrefix + "get/by-ids/{id}";
        public const string ShipmentValidatePath = APIPrefix + "validate/{id}";
        public const string ShipmentAssignPickupPath = APIPrefix + "assign/pickup";
        public const string ShipmentValidateThirdPartyPath = APIPrefix + "validate/thirdparty";
        public const string ShipmentByUserNamePath = APIPrefix + "get/{username}/{shipment}/{shipmentStatus}";
        public const string ShipmentValidateStatusPath = APIPrefix + "validate/status";
        #endregion

        #region Member
        public const string MemberDeletePath = APIPrefix + "{id}";
        public const string MemberByIdPath = APIPrefix + "get/{id}";
        public const string MemberByRolePath = APIPrefix + "get/by-role";
        public const string MemberSearchPath = APIPrefix + "search";
        public const string MemberChangePasswordPath = APIPrefix + "password/change";
        public const string MemberResetPasswordPath = APIPrefix + "password/reset/{userId}";
        public const string MemberChangeStationPath = APIPrefix + "update/station/{memberId}/{stationId}";
        public const string MemberChangeRolePath = APIPrefix + "update/role/{userId}/{roleId}";
        public const string MemberChangeActiveStatusPath = APIPrefix + "update/active/{memberId}";
        public const string MemberPath = APIPrefix;
        #endregion

        #region Image Upload
        public const string FileUploadPath = "upload";
        public const string FileGetImageByModNameModIdPath = "image/get/modname/id";
        public const string FileGetImageByModNameModIdsPath = "image/get/modname/ids";
        public const string FileGetImageByModNameModIdSeqPath = "image/get/modname/id/seq";
        public const string FileGetImageByModNamePagingPath = "image/get/modname/paging";
        public const string FileDeleteImageByIdPath = "image/delete/id";
        #endregion

        #region Mobile API Path
        public const string MobileAPIGetShipmentByMemberPath = $"api/mobile/get/shiment/by-member";
        public const string MobileAPIMarkPickupDonePath = $"api/mobile/update/pickup/done";
        public const string MobileAPIMarkPickupFailedPath = $"api/mobile/update/pickup/failed"; 
        public const string MobileAPIMarkPickupRescheduledPath = $"api/mobile/update/pickup/rescheduled";
        public const string MobileAPIMarkReadyForPickupPath = $"api/mobile/update/pickup/ready";
        public const string MobileAPIUploadPodImagePath = $"api/mobile/upload/pod/image";
        #endregion

        #endregion

        #region Error Type Message
        public const string ErrorType_InvalidDataSupplied = "InvalidDataSupplied";
        public const string ErrorType_UnauthorizedAccess = "UnauthorizedAccess";
        public const string ErrorType_NoDataSupplied = "NoDataSupplied";
        public const string ErrorType_InvalidCredentials = "InvalidCredentials";
        public const string ErrorType_UserAccountBlocked = "UserAccountBlocked";
        public const string ErrorType_UserAccountLocked = "UserAccountLocked";
        public const string ErrorType_UserAccountEmailNotVerified = "UserAccountEmailNotVerified";
        public const string ErrorType_InvalidConfigData = "InvalidConfigData"; 
        public const string ErrorType_UserNotFound = "UserNotFound";
        public const string ErrorType_InvalidModuleName = "InvalidModuleName";
        public const string ErrorType_ImageNotSelected = "ImageNotSelected";
        public const string Message_ImageNotSelected = "Image not selected!";
        public const string ErrorType_RecordNotFound = "RecordNotFound";
        public const string ErrorType_DeleteNotAllowed = "DeleteNotAllowed";
        public const string ErrorType_UpdateNotAllowed = "UpdateNotAllowed";
        public const string EmailAlreadyExist_Error = "EmailAlreadyExist!";
        public const string NewAndConfirmPasswordNotSame_Error = "NewAndConfirmPasswordNotSame!";
        public const string ErrorType_AlreadyDeleted = "AlreadyDeleted";
        public const string ErrorType_AlreadyExist = "AlreadyExist";
        public const string ErrorType_EmailAlreadyVerified = "EmailAlreadyVerified";
        public const string ErrorType_CustomerAlreadyExist= "CustomerAlreadyExist";
        public const string ErrorType_CustomerNotFoundError = "CustomerNotFoundError";
        public const string ErrorType_InvalidParameters = "InvalidParameters!";
        public const string ErrorType_InvalidGUID = "InvalidGUID!";
        public const string Error_UserIdNotPresentInHeader = "UserIdNotPresentInHeader!";
        public const string Error_UserIdHeaderNotPresentInRequest = "UserIdHeaderNotPresentInRequest!";
        public const string Error_UserIdInvalidPresentInHeader = "UserIdNotPresentInHeader!";
        public const string Error_CantCheckinAtSourceStation = "CantCheckedInAtDestinationStation!";
        public const string Error_CantCheckOutAtDestinationStation = "CantCheckOutAtDestinationStation!";
        public const string Error_ShipmentNumberNotFound = "ShipmentNumberNotFound!";
        public const string Error_ShipmentNotFound = "ShipmentNotFound!";
        public const string Error_SomeShipmentNoNotFound = "SomeShipmentNoNotFound!";
        public const string Error_InvalidShipmentNo = "InvalidShipmentNo!";
        public const string Error_InvalidShipmentStatus = "InvalidShipmentStatus!";
        public const string Error_InvalidShipmentStatusForThirdParty = "InvalidShipmentStatusForThirdParty!";
        public const string Error_SomeShipmentsAlreadyAddedToThirdParty = "SomeShipmentsAlreadyAddedToThirdParty!";
        public const string Error_InvalidShipmentStatusAssign = "InvalidShipmentStatusAssign!";
        public const string Error_InvalidCurrentShipmentStatus = "InvalidCurrentShipmentStatus!";
        public const string Error_ShipmentNotAssignedToYou = "ShipmentNotAssignedToYou!";
        public const string Error_ShipmentNumberAlreadyExist = "ShipmentNumberAlreadyExist!";
        public const string Error_EmailAlreadyRegistered = "Email is already registered";
        public const string Error_UnauthorizedAccess = "Unauthorized Access";
        public const string Error_PermissionNotMapped = "PermissionNotMapped";
        #endregion

        #region Error Message
        public const string Message_PermissionNotMapped = "Permission not mapped with user!";
        public const string Message_ShipmentNumberAlreadyExist = "Shipment number ({0}) already exist!";
        public const string Message_ShipmentNotAssignedToYou = "This shipment is not assigned to you!";
        public const string EmailAlreadyExist_Message = "Email already exist!";
        public const string NewAndConfirmPasswordNotSame_Message = "New & confirm password are not same!";
        public const string MasterDataTypeAlreadyExistError = "MasterDataTypeAlreadyExist!";
        public const string MasterDataTypeAlreadyExistMessage = "Master data type is already exist!";
        public const string InvalidEmail = "Email address is invalid!";
        public const string DataNotFoundError = "DataNotFound!";
        public const string DataNotFoundMessage = "Data does not exist!";
        public const string ContainerJourneyDetailsNotFound = "Container journey details not found!";
        public const string RecordAlreadyExistError = "RecordAlreadyExist!";
        public const string RecordAlreadyCancelledError = "RecordAlreadyCancelledError!";
        public const string RecordAlreadyCancelledMessage = "Record already cancelled!";
        public const string RecordAlreadyDeletedError = "RecordAlreadyDeletedError!";
        public const string RecordAlreadyDeletedMessage = "Record already deleted!";
        public const string InvalidMasterDataTypeError = "Invalid master data type!";
        public const string InvalidMasterDataTypeMessage = "Master data type is invalid!";
        public const string Error_RecordNotFound = "Record not found!";
        public const string Error_InvalidOldPassword = "Invalid old password";
        public const string UserNotFound_Message = "User isn't register with us!";
        public const string UserNotFound_Error= "UserNotFound";
        public const string InvalidCredentials_Error = "Error_InvalidCredentials";
        public const string InvalidCredentials_Message = "Wrong username/password!";
        public const string Error_UserAccountBlocked = "User's account is blocked!";
        public const string Error_UserAccountLocked = "User's account is locked!";
        public const string Error_UserAccountEmailNotVerified = "User's email is not varified!";
        public const string Error_EmailAlreadyVerified = "";
        public const string Error_CustomerNotFoundError = "Customer not found!";
        public const string Error_InvalidParameters= "Invalid parameters!";
        public const string Error_InvalidGUID = "Invalid GUID!";
        public const string Error_InvalidModuleName = "Invalid module name";
        public const string Error_ShipmentStatusShouldBeStored = "Shipment status should be stored!";
        public const string Message_UserIdNotPresentInHeader = "User id is not present in header!";
        public const string Error_UnableToSaveData = "UnableToSaveData!";
        public const string Error_ContainerAlreadyCheckedInAtStation = "ContainerAlreadyCheckedInAtStation!";
        public const string Error_ContainerAlreadyCheckedOutAtStation = "ContainerAlreadyCheckedOutAtStation!";
        public const string Error_ContainerIsNotClosed = "ContainerIsNotClosed!";
        public const string Error_ContainerAlreadyClosed = "ContainerAlreadyClosed!";
        public const string Error_ContainerClosedCantDelete = "ContainerClosedCantDelete!";
        public const string Error_LogisiticReasonAlreadyExist = "LogisiticReasonAlreadyExist";
        public const string Error_ThirdPartyCouierAlreadyExist = "Third party couier already exist";
        public const string Message_ContainerAlreadyCheckedInAtStation = "Container already checked-in at station!";
        public const string Message_ContainerAlreadyCheckedOutAtStation = "Container already checked-out at station!";
        public const string Message_UserIdInvalidPresentInHeader = "UserId is invalid present in header!";
        public const string Message_UserIdHeaderNotPresentInRequest = "Userid header not present in request!";
        public const string Message_CantCheckinAtSourceStation = "Can't check-in at source station!";
        public const string Message_CantCheckOutAtDestinationStation = "Can't check-out at destination station!";
        public const string Message_ContainerIsNotClosed = "Container is open, Please close the container!";
        public const string Message_ContainerAlreadyClosed = "Container is already closed!";
        public const string Message_UnableToSaveData = "Unable to save data!";
        public const string Message_SomeShipmentNoNotFound = "Some shipment number not found!";
        public const string Message_ShipmentNumberNotFound = "Shipment number not found!";
        public const string Message_ShipmentNotFound = "Shipment not found!";
        public const string Message_ContainerClosedCantDelete = "Container is closed, We cant delete container!";
        public const string Message_InvalidShipmentNo = "Invalid shipment Number!";
        public const string Message_InvalidShipmentStatus = "Invalid shipment status!";
        public const string Message_InvalidShipmentStatusForThirdParty = "Invalid shipment status for third party!";
        public const string Message_InvalidShipmentStatusAssign = "You are assigning invalid shipment status!";
        public const string Message_InvalidCurrentShipmentStatus = "Invalid current shipment status for Shipment no. : ";
        public const string Message_AppSettingKeyNotFound = "App setting key not found!";
        public const string Message_DeleteNotAllowed = "Delete not allowed";
        public const string Message_UpdateNotAllowed = "Update not allowed";

        public const string Message_LogisiticReasonAlreadyExist = "Logisitic reason already exist";
        public const string Message_ThirdPartyCouierAlreadyExist = "Third party couier already exist";

        public static string RecordAlreadyExistMessage(string recordType)
        {
            return $"Record already exist with same: {recordType}";
        }


        public static string Error_CustomerAlreadyExist (string? reason) {
            return "Customer already exist with same "+reason+" !";
        }
        #endregion

        #region Constant Values
        public const string ConstValue_UserId = "UserId";
        public const string ConstValue_CreatedAt = "CreatedAt";
        #endregion
    }
}
