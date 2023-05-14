using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Api.Domain.Dtos
{
    public class CredentialResponseDto
    {
        public string? Id { get; set; }

        public Roles roles {get; set;}
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? classId {get; set;}
    }
}