using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace API.DTO.API.DTO
{
    public class BagDTO
    {
        [MinLength(15)]
        [MaxLength(15)]
        public string? BagNumber { get; set; }

        public ICollection<ParcelBagDTO>? ParcelBags { get; set; }
        public LetterBagDTO? LetterBag { get; set; }
        public Guid? Id { get; set; }
    }
}