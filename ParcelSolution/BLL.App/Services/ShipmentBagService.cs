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
    public class ShipmentBagService :
        BaseEntityService<IAppUnitOfWork, IShipmentBagRepository, BLLMapper<DAL.App.DTO.ShipmentBag, BLL.App.DTO.ShipmentBag>,
            DAL.App.DTO.ShipmentBag, BLL.App.DTO.ShipmentBag>, IShipmentBagService
    {
        public ShipmentBagService(IAppUnitOfWork uow) : base(uow, uow.ShipmentBags, new BLLMapper<DAL.App.DTO.ShipmentBag, BLL.App.DTO.ShipmentBag>())
        {
        }
        
        public async Task<IEnumerable<ShipmentBag>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }
        
        public async Task<ShipmentBag> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }
        
    }
}