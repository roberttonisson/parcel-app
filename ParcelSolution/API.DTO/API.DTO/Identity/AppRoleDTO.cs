using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace API.DTO.API.DTO.Identity
{
    public class AppRoleDTO : IdentityRole<Guid>, IDomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string DisplayName { get; set; } = default!;
    }
}