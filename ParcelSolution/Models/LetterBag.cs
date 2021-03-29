using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace Models
{
    public class LetterBag : DomainEntityIdMetadata
    {
        [Required]
        public Guid BagId { get; set; } = default!;
        public Bag? Bag { get; set; }

        [Required]
        public int Count { get; set; } = default!;
        
        [Column(TypeName = "decimal(12,3)")]
        [Required]
        public decimal Weight { get; set; } = default!;
        
        [Column(TypeName = "decimal(12,2)")]
        [Required]
        public decimal Price { get; set; } = default!;
        
    }
}