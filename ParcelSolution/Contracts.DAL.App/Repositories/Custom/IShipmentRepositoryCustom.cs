using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories.Custom
{
    public interface IShipmentRepositoryCustom: IShipmentRepositoryCustom<Shipment>
    {
    }

    public interface IShipmentRepositoryCustom<TShipment>
    {
        Task<IEnumerable<TShipment>> GetAllShipmentsWithIncludedDataAsync(Guid? userId = null, bool noTracking = true);
        Task<TShipment> FindByShipmentNumber(string id, Guid? userId = null, bool noTracking = true);
        
    }
    
}