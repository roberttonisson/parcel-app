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
    public class LetterBagRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, Models.LetterBag, DAL.App.DTO.LetterBag>,
        ILetterBagRepository
    {
        public LetterBagRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Models.LetterBag, DAL.App.DTO.LetterBag>())
        {
        }

        public virtual async Task<IEnumerable<DTO.LetterBag>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Bag!);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.LetterBag> FirstOrDefaultAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Bag!)
                    .Where(x => x.Id == Id);

            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);
        }
    }
}