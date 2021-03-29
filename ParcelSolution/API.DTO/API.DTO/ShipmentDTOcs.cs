using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Extensions;

namespace API.DTO.API.DTO
{
    public class ShipmentDTO

    {
        [MinLength(10)]
        [MaxLength(10)]
        public string? ShipmentNumber { get; set; } = null;

        public Airport Airport { get; set; } = default!;

        [MinLength(6)]
        [MaxLength(6)]
        public string? FlightNumber { get; set; } = null;

        public DateTime FlightDate { get; set; } = default!;

        public bool Finalized { get; set; } = false;

        public ICollection<ShipmentBagDTO>? ShipmentBags { get; set; }
        public Guid? Id { get; set; }
    }
}