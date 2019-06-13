using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TStack.EntityFrameworkCore.Entity;

namespace TStack.Sql.EntityFrameworkCore.Repository
{
    public interface IEntityRepository<T>
        where T : class, IEfEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> expression = null);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        T GetItem(Expression<Func<T, bool>> expression);
        Task<T> GetItemAsync(Expression<Func<T, bool>> expression);
        T Insert(T Item);
        Task<T> InsertAsync(T Item);
        List<T> InsertAll(List<T> Items);
        Task<List<T>> InsertAllAsync(List<T> Items);
        T Update(T Item);
        Task<T> UpdateAsync(T Item);
        bool DeleteAll(List<T> Items);
        Task<bool> DeleteAllAsync(List<T> Items);
        bool Delete(T Item);
        Task<bool> DeleteAsync(T Item);
    }
}
