using System;

namespace Domain.Base
{
    public interface IDomainEntityId : IDomainEntityId<Guid>
    {
        
    }
    /*Interface for Domain Entity ID*/
    public interface IDomainEntityId<TKey>
        where TKey: IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
    
}