using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ShipmentBagRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, Models.ShipmentBag, DAL.App.DTO.ShipmentBag>,
        IShipmentBagRepository
    {
        public ShipmentBagRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Models.ShipmentBag, DAL.App.DTO.ShipmentBag>())
        {
        }
        
        public virtual async Task<IEnumerable<DTO.ShipmentBag>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Bag!);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.ShipmentBag> FirstOrDefaultAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(s => s.Shipment)
                .Include(b => b.Bag!)
                .Where(x => x.Id == Id);

            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);
        }
        
    }
}