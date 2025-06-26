using EnvanteriX.Application.Interfaces.Repositories;
using EnvanteriX.Domain.Common;
using EnvanteriX.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
    {
        private readonly EnvanteriXContext _context;

        public WriteRepository(EnvanteriXContext context)
        {
            _context = context;
        }
        private DbSet<T> Table { get => _context.Set<T>(); }

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task AddRangeAsync(IList<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }
        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }
        public async Task HardDeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }
    }
}
