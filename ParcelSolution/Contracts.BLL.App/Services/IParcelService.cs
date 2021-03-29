using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface IParcelService : IBaseEntityService<Parcel>, IParcelRepositoryCustom<Parcel>
    {
        Task<IEnumerable<Parcel>> GetAllAvailableParcels(Guid? userId = null, bool noTracking = true);
    }
}