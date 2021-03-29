using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories.Custom
{
    public interface IParcelRepositoryCustom: IParcelRepositoryCustom<Parcel>
    {
    }

    public interface IParcelRepositoryCustom<TParcel>
    {
        Task<IEnumerable<TParcel>> GetAllParcelsWithBags(Guid? userId = null, bool noTracking = true);
    }
    
}