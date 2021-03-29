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
    public class ParcelBagRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, Models.ParcelBag, DAL.App.DTO.ParcelBag>,
        IParcelBagRepository
    {
        public ParcelBagRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Models.ParcelBag, DAL.App.DTO.ParcelBag>())
        {
        }
        public virtual async Task<IEnumerable<DTO.ParcelBag>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Bag!)
                .Include(p => p.Parcel);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.ParcelBag> FirstOrDefaultAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Bag!)
                .Include(p => p.Parcel)
                .Where(x => x.Id == Id);

            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);
        }
        
        
    }
}