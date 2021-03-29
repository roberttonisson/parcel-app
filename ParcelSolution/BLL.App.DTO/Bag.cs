using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Bag : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MinLength(15)]
        [MaxLength(15)]
        [Required]
        public string BagNumber { get; set; } = default!;
        
        public ICollection<ParcelBag>? ParcelBags { get; set; }
        public LetterBag? LetterBag { get; set; }
    }
}