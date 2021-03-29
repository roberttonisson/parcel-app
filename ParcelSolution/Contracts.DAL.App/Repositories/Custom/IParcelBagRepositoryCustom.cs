using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories.Custom
{
    public interface IParcelBagRepositoryCustom: IParcelBagRepositoryCustom<ParcelBag>
    {
    }

    public interface IParcelBagRepositoryCustom<TParcelBag>
    {
        Task<IEnumerable<TParcelBag>> GetAllAsync(Guid? userId = null, bool noTracking = true);
        Task<TParcelBag> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true);
    }
    
}