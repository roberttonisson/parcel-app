using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Extensions;

namespace BLL.App.DTO
{
    public class Shipment : IDomainEntityId

    {
        public Guid Id { get; set; }
        
        [MinLength(10)]
        [MaxLength(10)]
        [Required]
        public string? ShipmentNumber { get; set; }
        
        public Airport Airport { get; set; } = default!;
        
        [MinLength(6)]
        [MaxLength(6)]
        [Required]
        public string? FlightNumber { get; set; }
        
        public DateTime FlightDate { get; set; } = default!;

        public bool Finalized { get; set; } = false;

        public ICollection<ShipmentBag>? ShipmentBags { get; set; }
        
    }
}