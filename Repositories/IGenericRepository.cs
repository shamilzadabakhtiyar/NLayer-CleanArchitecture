using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public interface IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
    {
        Task<bool> AnyAsync(TId id);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask<EntityEntry<T>> AddAsync(T entity);
        EntityEntry<T> Update(T entity);
        EntityEntry<T> Delete(T entity);
    }
}
