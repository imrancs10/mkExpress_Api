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

            #region Customer
            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
            CreateMap<PagingResponse<Customer>, PagingResponse<CustomerResponse>>();
            #endregion

            #region LogisticRegion
            CreateMap<LogisticRegionRequest, LogisticRegion>();
            CreateMap<LogisticRegion, LogisticRegionResponse>()
                .ForMember(des => des.Country, src => src.MapFrom(x => x.Country.Value))
                 .ForMember(des => des.Province, src => src.MapFrom(x => x.Province.Value))
                  .ForMember(des => des.City, src => src.MapFrom(x => x.City.Value))
                   .ForMember(des => des.Station, src => src.MapFrom(x => x.Station.Value))
                    .ForMember(des => des.District, src => src.MapFrom(x => x.District.Value??string.Empty))
                     .ForMember(des => des.ParentStation, src => src.MapFrom(x => x.ParentStation.Value));
            CreateMap<PagingResponse<LogisticRegion>, PagingResponse<LogisticRegionResponse>>();
            #endregion

            #region Member
            CreateMap<MemberRequest, Member>();
            CreateMap<Member, MemberResponse>();
            CreateMap<PagingResponse<Member>, PagingResponse<MemberResponse>>();
            #endregion

            #region Shipment
            CreateMap<ShipmentRequest, Shipment>();
            CreateMap<Shipment, ShipmentResponse>()
                  .ForMember(des => des.CustomerName, src => src.MapFrom(x => x.Customer.Name));
            CreateMap<PagingResponse<Shipment>, PagingResponse<ShipmentResponse>>();

            CreateMap<ShipmentDetailRequest, ShipmentDetail>();
            CreateMap<ShipmentDetail, ShipmentDetailResponse>()
                .ForMember(des => des.ToStore, src => src.MapFrom(x => x.ToStore.Value))
                .ForMember(des => des.FromStore, src => src.MapFrom(x => x.FromStore.Value))
                .ForMember(des => des.ShipperCity, src => src.MapFrom(x => x.ShipperCity.Value))
                .ForMember(des => des.ConsigneeCity, src => src.MapFrom(x => x.ConsigneeCity.Value));
            CreateMap<PagingResponse<ShipmentDetail>, PagingResponse<ShipmentDetailResponse>>();
                

            CreateMap<ShipmentTrackingRequest, ShipmentTracking>();
            CreateMap<ShipmentTracking, ShipmentTrackingResponse>();
            CreateMap<PagingResponse<ShipmentTracking>, PagingResponse<ShipmentTrackingResponse>>();
            #endregion

        }

        public static IMapper GetMapperConfig()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
            return config.CreateMapper();
        }
    }
}
