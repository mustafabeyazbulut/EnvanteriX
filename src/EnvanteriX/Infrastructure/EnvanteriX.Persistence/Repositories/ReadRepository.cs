﻿using EnvanteriX.Application.Interfaces.Repositories;
using EnvanteriX.Domain.Common;
using EnvanteriX.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace EnvanteriX.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityBase, new()
    {
        private readonly EnvanteriXContext _context;
        public ReadRepository(EnvanteriXContext context)
        {
            _context = context;
        }
        private DbSet<T> Table { get => _context.Set<T>(); }
        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking(); // performans için sadece veriyi çekiyoruz
            if (include is not null) queryable = include(queryable);
            if (predicate is not null) queryable = queryable.Where(predicate);
            if (orderby is not null) return await orderby(queryable).ToListAsync();

            return await queryable.ToListAsync();
        }
        public async Task<IList<T>> GetAllByPagingAsync(
                            Expression<Func<T, bool>>? predicate = null,
                            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                            Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null,
                            bool enableTracking = false,
                            int currentPage = 1,
                            int pageSize = 3)
        {
            IQueryable<T> queryable = Table;

            if (!enableTracking) queryable = queryable.AsNoTracking(); // Performans için takip edilmiyor
            if (include is not null) queryable = include(queryable);
            if (predicate is not null) queryable = queryable.Where(predicate);

            // Eğer orderby sağlanmadıysa varsayılan bir sıralama uygulayın
            if (orderby is null)
            {
                queryable = queryable.OrderBy(x => x); // Örnek sıralama (ID'ye göre)
            }

            return await queryable.Skip((currentPage - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking(); // performans için sadece veriyi çekiyoruz
            if (include is not null) queryable = include(queryable);

            //queryable.Where(predicate);

            return await queryable.FirstOrDefaultAsync(predicate);
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            Table.AsNoTracking();
            if (predicate is not null) Table.Where(predicate);

            return await Table.CountAsync();
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            if (!enableTracking) Table.AsNoTracking();

            return Table.Where(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
        }
    }
}
