using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class BagService :
        BaseEntityService<IAppUnitOfWork, IBagRepository, BLLMapper<DAL.App.DTO.Bag, BLL.App.DTO.Bag>,
            DAL.App.DTO.Bag, BLL.App.DTO.Bag>, IBagService
    {
        public BagService(IAppUnitOfWork uow) : base(uow, uow.Bags, new BLLMapper<DAL.App.DTO.Bag, BLL.App.DTO.Bag>())
        {
        }

    }
}