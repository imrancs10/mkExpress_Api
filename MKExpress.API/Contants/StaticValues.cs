namespace MKExpress.API.Contants
{
    public static class StaticValues
    {
        #region API Paths
        public const string APIPrefix = "v1/";

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


        #endregion

        #region Temple
        public const string TemplePath = "temple";
        public const string TempleGetByIdPath = "temple/get/{id}";
        public const string TempleGetByIdOrBarcodeIdPath = "barcodetemple/get/{barcodeId}";
        public const string TempleGetByYatraIdPath = "temple/get/yatra/{yatraId}";
        public const string TempleGetByPadavIdPath = "temple/get/padav/{padavId}";
        public const string TempleGetByYatraPadavIdPath = "temple/get/yatrapadav/{yatraId}/{padavId}";
        public const string TempleSearchPath = "temple/search";
        public const string TempleDeletePath = "temple/delete/{id}";
        public const string TempleCategoryPath = "get/templeCategories";
        public const string GenerateTempleQrCodePath = "temple/generate/temple/qr/code";
        public const string TempleUpdateFromExcelPath = "temple/update/excel";
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
        public const string ErrorType_AlreadyDeleted = "AlreadyDeleted";
        public const string ErrorType_AlreadyExist = "AlreadyExist";
        public const string ErrorType_EmailAlreadyVerified = "EmailAlreadyVerified";
        #endregion

        #region Error Message
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
        public const string InvalidMasterDataTypeError = "InvalidMasterDataTypeError!";
        public const string InvalidMasterDataTypeMessage = "Master data type is invalid!";
        public const string Error_InvalidOldPassword = "Error_InvalidOldPassword";
        public const string Error_UserNotFound = "Error_UserNotFound";
        public const string Error_InvalidCredentials = "Error_InvalidCredentials";
        public const string Error_UserAccountBlocked = "";
        public const string Error_UserAccountLocked = "";
        public const string Error_UserAccountEmailNotVerified = "";
        public const string Error_EmailAlreadyVerified = "";
        public static string RecordAlreadyExistMessage(string recordType)
        {
            return $"Record already exist with same: {recordType}";
        }
        #endregion

        #region Constant Values
        #endregion
    }
}
