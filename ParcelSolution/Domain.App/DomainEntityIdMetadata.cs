using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain
{
    public abstract class DomainEntityIdMetadata : DomainEntityIdMetadata<Guid>, IDomainEntityId
    {
        
    }

    /*Implements DomainEntityIdMetadata for metadata fields, also
     implements DomainEntityId for entity Id field*/
    public abstract class DomainEntityIdMetadata<TKey> : DomainEntityId<TKey>, IDomainEntityMetadata
        where TKey : IEquatable<TKey>
    {
        [MaxLength(256)]
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [MaxLength(256)]
        [JsonIgnore]
        public string? ChangedBy { get; set; }
        [JsonIgnore]
        [DataType(DataType.Time)]
        public DateTime ChangedAt { get; set; }
    }
}