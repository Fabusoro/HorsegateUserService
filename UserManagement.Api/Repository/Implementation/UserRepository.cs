using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Api.Domain;
using UserManagement.Api.Repository.Interface;

namespace UserManagement.Api.Repository.Implementation
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}