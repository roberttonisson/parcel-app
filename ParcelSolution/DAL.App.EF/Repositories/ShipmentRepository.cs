using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ShipmentRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, Models.Shipment, DAL.App.DTO.Shipment>,
        IShipmentRepository
    {
        public ShipmentRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Models.Shipment, DAL.App.DTO.Shipment>())
        {
        }
        
        public virtual async Task<IEnumerable<DTO.Shipment>> GetAllShipmentsWithIncludedDataAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(s => s.ShipmentBags!)
                .ThenInclude(sb => sb.Bag)
                .ThenInclude(b => b!.ParcelBags)
                .ThenInclude(pb => pb.Parcel)
                .Include(s => s.ShipmentBags!)
                .ThenInclude(sb => sb.Bag)
                .ThenInclude(b => b!.LetterBag);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public async Task<Shipment> FindByShipmentNumber(string id, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Where(s => s.ShipmentNumber == id)
                .AsSplitQuery()
                .Include(s => s.ShipmentBags!)
                .ThenInclude(sb => sb.Bag)
                .ThenInclude(b => b!.ParcelBags)
                .ThenInclude(pb => pb.Parcel)
                .Include(s => s.ShipmentBags!)
                .ThenInclude(sb => sb.Bag)
                .ThenInclude(b => b!.LetterBag);
                
            var shipment = await query.FirstOrDefaultAsync();
            var result = Mapper.Map(shipment);
            return result;
        }
    }
}