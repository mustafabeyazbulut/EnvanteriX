using EnvanteriX.Application.Interfaces.Repositories;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Persistence.Context;
using EnvanteriX.Persistence.Repositories;

namespace EnvanteriX.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EnvanteriXContext _context;

        public UnitOfWork(EnvanteriXContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public int Save() => _context.SaveChanges();

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(_context);

        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(_context);
    }
}
