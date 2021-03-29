using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using LetterBag = BLL.App.DTO.LetterBag;

namespace BLL.App.Services
{
    public class LetterBagService :
        BaseEntityService<IAppUnitOfWork, ILetterBagRepository, BLLMapper<DAL.App.DTO.LetterBag, BLL.App.DTO.LetterBag>,
            DAL.App.DTO.LetterBag, BLL.App.DTO.LetterBag>, ILetterBagService
    {
        public LetterBagService(IAppUnitOfWork uow) : base(uow, uow.LetterBags,
            new BLLMapper<DAL.App.DTO.LetterBag, BLL.App.DTO.LetterBag>())
        {
        }

        public async Task<IEnumerable<LetterBag>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<LetterBag> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }

        public async Task<bool> AddLetterBagToShipment(Guid shipmentId, LetterBag letterBagData,
            bool noTracking = true)
        {
            
            var bag = UOW.Bags.Add(new Bag() { });
            await UOW.SaveChangesAsync();
            var letterBag = Repository.Add(new DAL.App.DTO.LetterBag
            {
                BagId = bag.Id,
                Count = letterBagData.Count,
                Weight = letterBagData.Weight,
                Price = letterBagData.Price
            });
            await UOW.SaveChangesAsync();
            var sp = UOW.ShipmentBags.Add(new ShipmentBag
            {
                ShipmentId = shipmentId,
                BagId = bag.Id,
            });
            await UOW.SaveChangesAsync();

            return true;
        }
    }
}