using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IBagService Bags { get; }
        ILetterBagService LetterBags { get; }
        IParcelService Parcels { get; }
        IParcelBagService ParcelBags { get; }
        IShipmentService Shipments { get; }
        IShipmentBagService ShipmentBags { get; }
    }
}