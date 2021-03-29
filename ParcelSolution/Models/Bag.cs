using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Base;

namespace Models
{
    public class Bag : DomainEntityIdMetadata
    {
        [MinLength(15)]
        [MaxLength(15)]
        [Required]
        public string BagNumber { get; set; } = default!;
        
        public ICollection<ParcelBag>? ParcelBags { get; set; }
        public LetterBag? LetterBag { get; set; }
    }
}