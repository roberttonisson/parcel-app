using System;

namespace Domain.Base
{
    /*Interface for Domain Entities, gives metadata fields*/
    public interface IDomainEntityMetadata
    {
        string? CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        string? ChangedBy { get; set; }
        DateTime ChangedAt { get; set; }
    }
}