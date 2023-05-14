using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Api.Repository.Interface
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository {get;}
        IClassRepository ClassRepository {get;}
        Task Commit();
        Task CreateTransaction();
        void Dispose();
        Task Rollback();
        void SaveAsync();
    }
}