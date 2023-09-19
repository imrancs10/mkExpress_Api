namespace MKExpress.API.Contants
{
    public static class StaticValues
    {
        #region API Paths
        public const string APIPrefix = "v1/[controller]";

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
        public const string MasterGetDivisionPath = "get/divisions";
        public const string MasterGetPadavPath = "get/padavs";
        public const string MasterGetPadavByYatraIdPath = "get/padavs/{yatraId}";
        public const string MasterGetPadavBydPath = "padavs/get/{Id}";
        public const string MasterGetYatraPath = "get/yatras";
        public const string MasterYatraGetByIdPath = "yatra/get/{id}";
        public const string MasterGetSequencePath = "get/sequences";

        public const string MasterDivisionPath = "divisions";
        public const string MasterDataPath = "master/data";
        public const string MasterDataGetByTypesPath = "master/data/types";
        public const string MasterDataDropdownPath = "master/data/dropdown";
        public const string MasterDataByIdPath = "master/data/{id}";
        public const string MasterPadavPath = "padavs";
        public const string MasterYatraPath = "yatras";
        public const string MasterSequencePath = "sequences";
        public const string MasterDataNearByPlacesPath = "master/nearby/data";
        public const string MasterDataSearchPath = "master/search";

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
        public const string Error_InvalidDataSupplied = "Invalid data supplied";
        public const string Error_NoDataSupplied = "No data supplied";
        public const string Error_InvalidCredentials = "Wrong username/password";
        public const string Error_UserNotFound = "User is not registered.";
        public const string Error_InvalidOldPassword = "Wrong old password";
        public const string Error_UserAccountBlocked = "User account is blocked";
        public const string Error_UserAccountLocked = "User account is locked";
        public const string Error_UserAccountEmailNotVerified = "User account email is not verified";
        public const string Error_InvalidConfigData = "Some appSetting config key missing/contains invalid data";
        public const string Error_InvalidModuleName = "Invalid modile name";
        public const string Error_ImageNotSelected = "Image not selected";
        public const string Error_RecordNotFound = "Record not found";
        public const string Error_AlreadyDeleted = "Record is already deleted";
        public const string Error_AlreadyExist = "Record is already exist";
        public const string Error_EmailAlreadyRegistered = "This email is already registered";
        public const string Error_EmailAlreadyVerified = "This email is already verified";
        #endregion

        #region Constant Values
        #endregion
    }
}
