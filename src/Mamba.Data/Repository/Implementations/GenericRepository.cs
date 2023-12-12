using Mamba.Core.Models;
using Mamba.Core.Repository.Interfaces;
using Mamba.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Data.Repository.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
                     where TEntity : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes)
        {
            var query = GetQuery(includes);

            return expression is not null
                      ? await query.Where(expression).ToListAsync()
                      : await query.ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes)
        {
            var query = GetQuery(includes);

            return expression is not null
                      ? await query.Where(expression).FirstOrDefaultAsync()
                      : await query.FirstOrDefaultAsync();
        }
        private IQueryable<TEntity> GetQuery(string[] includes)
        {
            var query = Table.AsQueryable();

            if (includes is not null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }

    }
}
