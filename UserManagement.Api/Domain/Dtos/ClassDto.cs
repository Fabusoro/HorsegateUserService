using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Api.Domain.Dtos
{
    public class ClassDto
    {
        public string Id {get; set;} = Guid.NewGuid().ToString();
        public string? Name {get; set;}
    }
}