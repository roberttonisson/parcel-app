using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories.Custom
{
    public interface IShipmentBagRepositoryCustom: IShipmentBagRepositoryCustom<ShipmentBag>
    {
    }

    public interface IShipmentBagRepositoryCustom<TShipmentBag>
    {
        Task<IEnumerable<TShipmentBag>> GetAllAsync(Guid? userId = null, bool noTracking = true);
        Task<TShipmentBag> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true);
    }
    
}