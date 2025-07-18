﻿using EnvanteriX.Domain.Common;

namespace EnvanteriX.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T : class, IEntityBase, new()
    {
        Task AddAsync(T entity);

        Task AddRangeAsync(IList<T> entities);

        Task<T> UpdateAsync(T entity);

        Task HardDeleteAsync(T entity);
    }
}
