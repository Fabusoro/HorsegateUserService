using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Api.Domain.Dtos
{
    public class UpdateDto
    {
        public string? FirstName {get; set;}
        public string? LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email {get; set;}
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber{get; set;} 
        public string? ClassId {get; set;}
    }
}