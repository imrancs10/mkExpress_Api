using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Request.ImageStore;
using MKExpress.API.DTO.Response;
using MKExpress.API.DTO.Response.Image;
using MKExpress.API.Models;

namespace MKExpress.API.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Login

            #endregion

            #region User
            CreateMap<User, UserResponse>();
            CreateMap<UserRequest, User>()
                .ForMember(des => des.EmailVerificationCode, src => src.MapFrom(x => Guid.NewGuid().ToString()))
                .ForMember(des => des.EmailVerificationCodeExpireOn, src => src.MapFrom(x => DateTime.Now.AddHours(24)));
            #endregion

            #region Image
            CreateMap<ImageStore, ImageStoreResponse>();
            CreateMap<ImageStore, ImageStoreWithName>();
            CreateMap<ImageStoreWithName, ImageStoreWithNameResponse>();
            CreateMap<ImageStoreRequest, ImageStore>();
            CreateMap<FileUploadQueryRequest, FileUploadRequest>();
            #endregion

            #region Master Data
            CreateMap<MasterDataRequest, MasterData>();
            CreateMap<MasterData,MasterDataResponse>();
            CreateMap<MasterData, MasterDataResponse>()
                 .ForMember(des => des.MasterDataTypeCode, src => src.MapFrom(x=>x.MasterDataType));
            CreateMap<MasterDataTypeRequest, MasterDataType>();
            CreateMap<MasterDataType, MasterDataTypeResponse>();
            CreateMap<PagingResponse<MasterDataType>, PagingResponse<MasterDataTypeResponse>>();
            CreateMap<PagingResponse<MasterData>, PagingResponse<MasterDataResponse>>();
            #endregion

        }

        public static IMapper GetMapperConfig()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
            return config.CreateMapper();
        }
    }
}
