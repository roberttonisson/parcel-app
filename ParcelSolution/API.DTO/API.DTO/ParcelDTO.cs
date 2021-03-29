using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace API.DTO.API.DTO
{
    public class ParcelDTO

    {
        [MinLength(9)]
        [MaxLength(9)]
        public string? ParcelNumber { get; set; }

        [MaxLength(100)] public string? Recipient { get; set; }

        [MinLength(2)]
        [MaxLength(2)]
        public string? Destination { get; set; }

        [Column(TypeName = "decimal(12,3)")]
        public decimal Weight { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        public ICollection<ParcelBagDTO>? ParcelBags { get; set; }
        
        public Guid? Id { get; set; }
    }
}