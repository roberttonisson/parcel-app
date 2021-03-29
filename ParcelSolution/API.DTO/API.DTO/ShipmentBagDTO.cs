using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace API.DTO.API.DTO
{
    public class ShipmentBagDTO
    {
        public Guid ShipmentId { get; set; } = default!;
        public ShipmentDTO? Shipment { get; set; }

        public Guid BagId { get; set; } = default!;
        public BagDTO? Bag { get; set; }
        
        public Guid? Id { get; set; }
    }
}