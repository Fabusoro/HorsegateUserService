using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Api.Domain;
using UserManagement.Api.Repository.Interface;

namespace UserManagement.Api.Repository.Implementation
{
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        public ClassRepository(AppDbContext context) : base(context)
        {
        }
    }
}