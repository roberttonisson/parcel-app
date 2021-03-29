using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.DTO.Identity
{
    public class AppRole : IdentityRole<Guid>, IDomainEntityId 
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string DisplayName { get; set; } = default!;
    }
}