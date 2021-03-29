using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        public IBagRepository Bags =>
            GetRepository<IBagRepository>(() => new BagRepository(UOWDbContext));

        public ILetterBagRepository LetterBags =>
            GetRepository<ILetterBagRepository>(() => new LetterBagRepository(UOWDbContext));

        public IParcelRepository Parcels =>
            GetRepository<IParcelRepository>(() => new ParcelRepository(UOWDbContext));

        public IParcelBagRepository ParcelBags =>
            GetRepository<IParcelBagRepository>(() => new ParcelBagRepository(UOWDbContext));

        public IShipmentRepository Shipments =>
            GetRepository<IShipmentRepository>(() => new ShipmentRepository(UOWDbContext));

        public IShipmentBagRepository ShipmentBags =>
            GetRepository<IShipmentBagRepository>(() => new ShipmentBagRepository(UOWDbContext));
        
    }
}