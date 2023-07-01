using MyOwnORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnORM.Interfaces
{
    public interface IDbSet<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);

        Task<List<T>> AddRangeAsync(List<T> entities);

        Task RemoveAsync(T entity);

        Task<T> FindAsync(int id);
        
        Task<T> FirstOrDefault();

        Task<List<T>> ToList();

        Task<int> Count();

        Task<bool> Any();

        Task<IQueryable<T>> Where(Func<T, bool> predicate);
    }
}