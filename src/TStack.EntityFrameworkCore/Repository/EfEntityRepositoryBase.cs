using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TStack.EntityFrameworkCore.Entity;

namespace TStack.Sql.EntityFrameworkCore.Repository
{
    public abstract class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEfEntity, new()
        where TContext : DbContext, new()
    {
        private readonly TContext _context;
        public EfEntityRepositoryBase()
        {
            _context = new TContext();
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> Filter = null)
        {
            using (_context)
            {
                return (Filter == null ? _context.Set<TEntity>().ToList() : _context.Set<TEntity>().Where(Filter).ToList());
            }
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> Filter = null)
        {
            using (_context)
            {
                return await (Filter == null ? _context.Set<TEntity>().ToListAsync() : _context.Set<TEntity>().Where(Filter).ToListAsync());
            }
        }
        public TEntity GetItem(Expression<Func<TEntity, bool>> Filter)
        {
            using (_context)
            {
                return _context.Set<TEntity>().Where(Filter).FirstOrDefault();
            }
        }
        public async Task<TEntity> GetItemAsync(Expression<Func<TEntity, bool>> Filter)
        {
            using (_context)
            {
                return await _context.Set<TEntity>().Where(Filter).FirstOrDefaultAsync();
            }
        }
        public TEntity Insert(TEntity Item)
        {
            using (_context)
            {
                var AddedEntity = _context.Entry(Item);
                AddedEntity.State = EntityState.Added;
                _context.SaveChanges();
                return Item;
            }
        }
        public async Task<TEntity> InsertAsync(TEntity Item)
        {
            return await Task.Run(async () =>
             {
                 using (_context)
                 {
                     var AddedEntity = _context.Entry(Item);
                     AddedEntity.State = EntityState.Added;
                     await _context.SaveChangesAsync();
                     return Item;
                 }
             });
        }
        public List<TEntity> InsertAll(List<TEntity> Items)
        {
            throw new NotImplementedException();
        }
        public Task<List<TEntity>> InsertAllAsync(List<TEntity> Items)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity Item)
        {
            using (_context)
            {
                var UpdatedEntity = _context.Entry(Item);
                UpdatedEntity.State = EntityState.Modified;
                _context.SaveChanges();
                return Item;
            }
        }
        public async Task<TEntity> UpdateAsync(TEntity Item)
        {
            return await Task.Run(async () =>
            {
                using (_context)
                {
                    var UpdatedEntity = _context.Entry(Item);
                    UpdatedEntity.State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Item;
                }
            });
        }
        public bool Delete(TEntity Item)
        {
            using (_context)
            {
                var DeletedEntity = _context.Entry(Item);
                DeletedEntity.State = EntityState.Deleted;
                _context.SaveChanges();
            }
            return true;
        }
        public async Task<bool> DeleteAsync(TEntity Item)
        {
            return await Task.Run(async () =>
            {
                using (_context)
                {
                    var DeletedEntity = _context.Entry(Item);
                    DeletedEntity.State = EntityState.Deleted;
                    await _context.SaveChangesAsync();
                }

                return true;
            });
        }
        public bool DeleteAll(List<TEntity> Items)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteAllAsync(List<TEntity> Items)
        {
            throw new NotImplementedException();
        }
    }
}
