using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Api.Domain.Dtos
{
    public class RegistrationDto
    {
        [Required]
        public string FirstName {get; set;}
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email {get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string PhoneNumber{get; set;}        
        [Required]
        public string ClassId { get; set; }     
    }
}