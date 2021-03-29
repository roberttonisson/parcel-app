using System;
using Domain.Base;

namespace Domain
{
    public abstract class DomainEntityId : DomainEntityId<Guid>, IDomainEntityId
    {
        
    }

    /*Implementation for Domain Entity Id, gives Id field when implemented*/
    public abstract class DomainEntityId<TKey> : IDomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
    }
}