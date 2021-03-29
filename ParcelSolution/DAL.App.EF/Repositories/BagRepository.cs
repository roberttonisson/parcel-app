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
    public class BagRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, Models.Bag, DAL.App.DTO.Bag>,
        IBagRepository
    {
        public BagRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Models.Bag, DAL.App.DTO.Bag>())
        {
        }

    }
}