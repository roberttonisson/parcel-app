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
    public class ParcelRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, Models.Parcel, DAL.App.DTO.Parcel>,
        IParcelRepository
    {
        public ParcelRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Models.Parcel, DAL.App.DTO.Parcel>())
        {
        }
        
        public virtual async Task<IEnumerable<DTO.Parcel>> GetAllParcelsWithBags(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(p => p.ParcelBags!);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
        
    }
}