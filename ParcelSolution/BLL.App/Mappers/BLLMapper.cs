using AutoMapper;
using DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        { 
            // add more mapping configurations
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Bag, BLL.App.DTO.Bag>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Parcel, BLL.App.DTO.Parcel>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.LetterBag, BLL.App.DTO.LetterBag>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ParcelBag, BLL.App.DTO.ParcelBag>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Shipment, BLL.App.DTO.Shipment>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ShipmentBag, BLL.App.DTO.ShipmentBag>();

            // create Mapper based on selected configurations
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}