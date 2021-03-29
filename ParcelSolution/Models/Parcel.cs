using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;
using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class Parcel : DomainEntityIdMetadata

    {
        [MinLength(10)]
        [MaxLength(10)]
        [Required]
        public string ParcelNumber { get; set; } = default!;

        [MaxLength(100)]
        [Required]
        public string Recipient { get; set; } = default!;
        
        [MinLength(2)]
        [MaxLength(2)]
        [Required]
        public string Destination { get; set; } = default!;
        
        [Column(TypeName = "decimal(12,3)")]
        [Required]
        public decimal Weight { get; set; } = default!;
        
        [Column(TypeName = "decimal(12,2)")]
        [Required]
        public decimal Price { get; set; } = default!;
        
        public ICollection<ParcelBag>? ParcelBags { get; set; }
        
    }
}