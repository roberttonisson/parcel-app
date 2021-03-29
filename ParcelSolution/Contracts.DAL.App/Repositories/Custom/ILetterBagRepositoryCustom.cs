using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories.Custom
{
    public interface ILetterBagRepositoryCustom: ILetterBagRepositoryCustom<LetterBag>
    {
    }

    public interface ILetterBagRepositoryCustom<TLetterBag>
    {
        Task<IEnumerable<TLetterBag>> GetAllAsync(Guid? userId = null, bool noTracking = true);
        Task<TLetterBag> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true); 
    }
    
}