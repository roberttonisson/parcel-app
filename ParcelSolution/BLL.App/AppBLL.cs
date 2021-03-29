using System;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }

        public IBagService Bags =>
            GetService<IBagService>(() => new BagService(UOW));

        public ILetterBagService LetterBags =>
            GetService<ILetterBagService>(() => new LetterBagService(UOW));

        public IParcelService Parcels =>
            GetService<IParcelService>(() => new ParcelService(UOW));

        public IParcelBagService ParcelBags =>
            GetService<IParcelBagService>(() => new ParcelBagService(UOW));

        public IShipmentService Shipments =>
            GetService<IShipmentService>(() => new ShipmentService(UOW));

        public IShipmentBagService ShipmentBags =>
            GetService<IShipmentBagService>(() => new ShipmentBagService(UOW));
        
    }
}