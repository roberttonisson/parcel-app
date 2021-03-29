using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Parcel : IDomainEntityId

    {
        public Guid Id { get; set; }
        
        [MinLength(9)]
        [MaxLength(9)]
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