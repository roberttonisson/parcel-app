using System;
using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Base;

namespace Models
{
    public class ShipmentBag : DomainEntityIdMetadata
    {
        [Required]
        public Guid ShipmentId { get; set; } = default!;
        public Shipment? Shipment { get; set; }
        
        [Required]
        public Guid BagId { get; set; } = default!;
        public Bag? Bag { get; set; }

    }
}