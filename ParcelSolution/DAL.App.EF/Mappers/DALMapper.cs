using AutoMapper;
using DAL.Base.Mappers;
using Models.Identity;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // add more mapping configurations
            MapperConfigurationExpression.CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<Models.Bag, DAL.App.DTO.Bag>();
            MapperConfigurationExpression.CreateMap<Models.Parcel, DAL.App.DTO.Parcel>();
            MapperConfigurationExpression.CreateMap<Models.LetterBag, DAL.App.DTO.LetterBag>();
            MapperConfigurationExpression.CreateMap<Models.ParcelBag, DAL.App.DTO.ParcelBag>();
            MapperConfigurationExpression.CreateMap<Models.Shipment, DAL.App.DTO.Shipment>();
            MapperConfigurationExpression.CreateMap<Models.ShipmentBag, DAL.App.DTO.ShipmentBag>();

            // create Mapper based on selected configurations
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}