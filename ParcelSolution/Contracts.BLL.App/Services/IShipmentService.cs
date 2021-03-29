using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface IShipmentService : IBaseEntityService<Shipment>, IShipmentRepositoryCustom<Shipment>
    {
        Shipment? GetSingleNonFinalizedShipment(string number, Guid? userId = null, bool noTracking = true);
    }
}