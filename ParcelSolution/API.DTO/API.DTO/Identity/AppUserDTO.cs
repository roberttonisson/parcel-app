using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace API.DTO.API.DTO.Identity
{
    public class AppUserDTO : IdentityUser<Guid>, IDomainEntityId
    {
        // add your own fields to User
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;

        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;

        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; }
    }
}