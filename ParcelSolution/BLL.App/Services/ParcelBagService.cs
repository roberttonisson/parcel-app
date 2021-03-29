using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.Mappers;

namespace BLL.App.Services
{
    public class ParcelBagService :
        BaseEntityService<IAppUnitOfWork, IParcelBagRepository, BLLMapper<DAL.App.DTO.ParcelBag, BLL.App.DTO.ParcelBag>,
            DAL.App.DTO.ParcelBag, BLL.App.DTO.ParcelBag>, IParcelBagService
    {
        public ParcelBagService(IAppUnitOfWork uow) : base(uow, uow.ParcelBags, new BLLMapper<DAL.App.DTO.ParcelBag, BLL.App.DTO.ParcelBag>())
        {
        }
        
        public async Task<IEnumerable<ParcelBag>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }
        
        public async Task<ParcelBag> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }
    }
}