using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Api.Domain.Dtos
{
    public class RegistrationResponseDto
    {
        public string? id {get; set;}
        public string? FirstName {get; set;}
        public string? LastName { get; set; }        
        public string? Email {get; set;}
        public Class? Class {get; set;}
        public Roles roles {get; set;}
    }
}