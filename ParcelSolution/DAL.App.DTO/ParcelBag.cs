using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ParcelBag : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Required]
        public Guid BagId { get; set; } = default!;
        public Bag? Bag { get; set; }
        
        [Required]
        public Guid ParcelId { get; set; } = default!;
        public Parcel? Parcel { get; set; }
        
    }
}