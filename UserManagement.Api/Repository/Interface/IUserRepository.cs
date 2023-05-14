using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Api.Domain;

namespace UserManagement.Api.Repository.Interface
{
    public interface IUserRepository: IGenericRepository<Users>
    {
    }
}