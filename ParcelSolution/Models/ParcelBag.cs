using System;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace Models
{
    public class ParcelBag : DomainEntityIdMetadata
    {
        [Required]
        public Guid BagId { get; set; } = default!;
        public Bag? Bag { get; set; }
        
        [Required]
        public Guid ParcelId { get; set; } = default!;
        public Parcel? Parcel { get; set; }
        
    }
}