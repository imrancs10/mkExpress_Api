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
        #endregion

        #region Customer API Path
        public const string CustomerDeletePath = APIPrefix + "{id}";
        public const string CustomerByIdPath = APIPrefix + "get/{id}";
        public const string CustomerByContactNoPath = APIPrefix + "get/by-contact";
        public const string CustomerSearchPath = APIPrefix + "search";
        public const string CustomerPath = APIPrefix;
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
        #endregion

        #region Member
        public const string MemberDeletePath = APIPrefix + "{id}";
        public const string MemberByIdPath = APIPrefix + "get/{id}";
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

        #endregion

        #region Error Type Message
        public const string ErrorType_InvalidDataSupplied = "InvalidDataSupplied";
        public const string ErrorType_NoDataSupplied = "NoDataSupplied";
        public const string ErrorType_InvalidCredentials = "InvalidCredentials";
        public const string ErrorType_UserAccountBlocked = "UserAccountBlocked";
        public const string ErrorType_UserAccountLocked = "UserAccountLocked";
        public const string ErrorType_UserAccountEmailNotVerified = "UserAccountEmailNotVerified";
        public const string ErrorType_InvalidConfigData = "InvalidConfigData"; 
        public const string ErrorType_UserNotFound = "UserNotFound";
        public const string ErrorType_InvalidModuleName = "InvalidModuleName";
        public const string ErrorType_ImageNotSelected = "ImageNotSelected";
        public const string ErrorType_RecordNotFound = "RecordNotFound";
        public const string EmailAlreadyExist_Error = "EmailAlreadyExist!";
        public const string NewAndConfirmPasswordNotSame_Error = "NewAndConfirmPasswordNotSame!";
        public const string ErrorType_AlreadyDeleted = "AlreadyDeleted";
        public const string ErrorType_AlreadyExist = "AlreadyExist";
        public const string ErrorType_EmailAlreadyVerified = "EmailAlreadyVerified";
        public const string ErrorType_CustomerAlreadyExist= "CustomerAlreadyExist";
        public const string ErrorType_CustomerNotFoundError = "CustomerNotFoundError";
        public const string ErrorType_InvalidParameters = "InvalidParameters!";
        public const string ErrorType_InvalidGUID = "InvalidGUID!";
        public const string ErrorType_UserIdNotPresentInHeader = "UserIdNotPresentInHeader!";
        #endregion

        #region Error Message
        public const string EmailAlreadyExist_Message = "Email already exist!";
        public const string NewAndConfirmPasswordNotSame_Message = "New & confirm password are not same!";
        public const string MasterDataTypeAlreadyExistError = "MasterDataTypeAlreadyExist!";
        public const string MasterDataTypeAlreadyExistMessage = "Master data type is already exist!";
        public const string InvalidEmail = "Email address is invalid!";
        public const string DataNotFoundError = "DataNotFound!";
        public const string DataNotFoundMessage = "Data does not exist!";
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
        public const string Error_CustomerAlreadyExist = "Customer already exist!";
        public const string Error_CustomerNotFoundError = "Customer not found!";
        public const string Error_InvalidParameters= "Invalid parameters!";
        public const string Error_InvalidGUID = "Invalid GUID!";
        public const string Error_UserIdNotPresentInHeader = "User id is not present in header!";

        public static string RecordAlreadyExistMessage(string recordType)
        {
            return $"Record already exist with same: {recordType}";
        }
        #endregion

        #region Constant Values
        public const string ConstValue_UserId = "UserId";
        public const string ConstValue_CreatedAt = "CreatedAt";
        #endregion
    }
}
