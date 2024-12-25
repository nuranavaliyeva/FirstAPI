﻿using FirstAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FirstAPI.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();

        }

        public IQueryable<T> GetAll(
            Expression<Func<T,
            bool>>? expression = null,
            int skip = 0,
            int take = 0,
            Expression<Func<T, object>>? orderExpression = null,
            bool isDescending = false,
            bool isTracking = false,
            params string[]? includes)
        {

            IQueryable<T> query = _table;

            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includes != null)
            {
                for(int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            if(orderExpression != null)
            {
                if (isDescending)
                {
                    query = query.OrderByDescending(orderExpression);
                }
                else
                {
                    query = query.OrderBy(orderExpression);
                }
            }
            query = query.Skip(skip);
            if(take != 0)
            {
                query = query.Take(take);
            }
           
           return isTracking?query:query.AsNoTracking(); 

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(T entity)
        {
           await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public void Update(T entity)
        {
           _table.Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
