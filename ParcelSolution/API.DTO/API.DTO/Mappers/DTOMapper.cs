using API.DTO.API.DTO.Identity;
using AutoMapper;
using DAL.Base.Mappers;

namespace API.DTO.API.DTO.Mappers
{
    public class DTOMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DTOMapper()
        {
            // add more mapping configurations
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Identity.AppUser, AppUserDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Bag, BagDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Parcel, ParcelDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.LetterBag, LetterBagDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ParcelBag, ParcelBagDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Shipment, ShipmentDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ShipmentBag, ShipmentBagDTO>();

            // create Mapper based on selected configurations
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}