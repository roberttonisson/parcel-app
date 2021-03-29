using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace API.DTO.API.DTO
{
    public class ParcelBagAddDTO
    {
        public Guid ShipmentId { get; set; } = default!;

        public Guid ParcelId { get; set; } = default!;

    }
}