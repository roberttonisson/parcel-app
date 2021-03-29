using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace API.DTO.API.DTO
{
    public class ParcelBagDTO
    {
        public Guid BagId { get; set; } = default!;
        public BagDTO? Bag { get; set; }
        public Guid ParcelId { get; set; } = default!;
        public ParcelDTO? Parcel { get; set; }

        public Guid? Id { get; set; }
    }
}