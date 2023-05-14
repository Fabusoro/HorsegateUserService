using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UserManagement.Api.Domain
{
    public class Users : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }        
        public bool IsActive { get; set; }    
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset UpdatedAt { get; set; }
        public string? ImageUrl { get; set; }
        public Roles roles {get; set;}
        public string? ClassId { get; set; }
        public Class? Class { get; set; }
        public bool IsPaid {get; set;} = false;
    }
}