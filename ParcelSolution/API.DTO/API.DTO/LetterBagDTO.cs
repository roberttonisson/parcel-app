using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace API.DTO.API.DTO
{
    public class LetterBagDTO
    {
        public Guid BagId { get; set; } = default!;

        public BagDTO? Bag { get; set; }

        public int Count { get; set; } = default!;

        [Column(TypeName = "decimal(12,3)")]
        public decimal Weight { get; set; } = default!;

        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; } = default!;

        public Guid? Id { get; set; }
    }
}