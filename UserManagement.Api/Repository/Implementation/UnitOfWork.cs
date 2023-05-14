using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using UserManagement.Api.Repository.Interface;

namespace UserManagement.Api.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposedValue;
        private readonly AppDbContext _context;
        private IDbContextTransaction _objTransaction;
        private IUserRepository _userRepository;
        private IClassRepository _classRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public IClassRepository ClassRepository => _classRepository ??= new ClassRepository(_context);


        public async Task CreateTransaction()
        {
            _objTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _objTransaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _objTransaction?.RollbackAsync();
            await _objTransaction.DisposeAsync();
        }


        public void SaveAsync()
        {
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}