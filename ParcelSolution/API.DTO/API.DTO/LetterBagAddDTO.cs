using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace API.DTO.API.DTO
{
    public class LetterBagAddDTO
    {
        public Guid ShipmentId { get; set; } = default!;
        
        public int Count { get; set; } = default!;

        [Column(TypeName = "decimal(12,3)")]
        public decimal Weight { get; set; } = default!;

        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; } = default!;
    }
}