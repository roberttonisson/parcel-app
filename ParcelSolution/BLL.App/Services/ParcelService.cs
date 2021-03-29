using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ParcelService :
        BaseEntityService<IAppUnitOfWork, IParcelRepository, BLLMapper<DAL.App.DTO.Parcel, BLL.App.DTO.Parcel>,
            DAL.App.DTO.Parcel, BLL.App.DTO.Parcel>, IParcelService
    {
        public ParcelService(IAppUnitOfWork uow) : base(uow, uow.Parcels, new BLLMapper<DAL.App.DTO.Parcel, BLL.App.DTO.Parcel>())
        {
        }
        
        public async Task<IEnumerable<Parcel>> GetAllAvailableParcels(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllParcelsWithBags(userId, noTracking))
                .Where(p=> !(p.ParcelBags?.Count > 0))
                .Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<Parcel>> GetAllParcelsWithBags(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllParcelsWithBags(userId, noTracking)).Select(e => Mapper.Map(e));
        }
    }
}