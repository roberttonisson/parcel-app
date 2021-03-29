using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace BLL.App.DTO
{
    public class LetterBag : IDomainEntityId
    {
        public Guid Id { get; set; }
        
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