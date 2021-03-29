using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ShipmentBag : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Required]
        public Guid ShipmentId { get; set; } = default!;
        public Shipment? Shipment { get; set; }
        
        [Required]
        public Guid BagId { get; set; } = default!;
        public Bag? Bag { get; set; }

    }
}