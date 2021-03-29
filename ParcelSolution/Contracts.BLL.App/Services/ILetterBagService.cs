using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface ILetterBagService : IBaseEntityService<LetterBag>, ILetterBagRepositoryCustom<LetterBag>
    {
        Task<bool> AddLetterBagToShipment(Guid shipmentId, LetterBag letterBagData, bool noTracking = true);
    }
}