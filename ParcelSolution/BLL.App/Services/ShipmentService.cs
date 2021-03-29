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
    public class ShipmentService :
        BaseEntityService<IAppUnitOfWork, IShipmentRepository, BLLMapper<DAL.App.DTO.Shipment, BLL.App.DTO.Shipment>,
            DAL.App.DTO.Shipment, BLL.App.DTO.Shipment>, IShipmentService
    {
        public ShipmentService(IAppUnitOfWork uow) : base(uow, uow.Shipments, new BLLMapper<DAL.App.DTO.Shipment, BLL.App.DTO.Shipment>())
        {
        }
        
        public async Task<IEnumerable<Shipment>> GetAllShipmentsWithIncludedDataAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllShipmentsWithIncludedDataAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }
        public Shipment? GetSingleNonFinalizedShipment(String number, Guid? userId = null, bool noTracking = true)
        {
            var shipment = Repository.FindByShipmentNumber(number, userId, noTracking).Result;
            return !shipment.Finalized ? Mapper.Map(shipment) : null;
        }

        public async Task<Shipment> FindByShipmentNumber(string id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FindByShipmentNumber(id, userId, noTracking));
        }
    }
}