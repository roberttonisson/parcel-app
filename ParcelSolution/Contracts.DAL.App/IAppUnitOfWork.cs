using System;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        IBagRepository Bags { get; }
        ILetterBagRepository LetterBags { get; }
        IParcelRepository Parcels { get; }
        IParcelBagRepository ParcelBags { get; }
        IShipmentRepository Shipments { get; }
        IShipmentBagRepository ShipmentBags { get; }
    }
}