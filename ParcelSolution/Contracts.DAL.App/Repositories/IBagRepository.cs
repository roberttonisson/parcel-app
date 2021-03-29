using Contracts.DAL.App.Repositories.Custom;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IBagRepository  : IBaseRepository<Bag>, IBagRepositoryCustom
    {
        
    }
}