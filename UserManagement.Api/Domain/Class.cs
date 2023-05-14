using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Api.Domain
{
    public class Class
    {
        public string Id {get; set;} = Guid.NewGuid().ToString();
        public string? Name {get; set;}
        public DateTimeOffset CreatedAt {get; set;} = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt {get; set;}      
        public ICollection<Users>? Users {get; set;}
    }
}