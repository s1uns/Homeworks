using MyOwnORM.Enums;
using MyOwnORM.Models;
using MyOwnORM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnORM.Interfaces
{
    public interface IDbContext<T> where T : BaseEntity
    {
        

        Task<T> UpdateAsync(int entityId, T newEntity);
        
        Task<T> RemoveAsync(int entityId);
        
        Task<NewEntity<T>> ChangeStateAsync(T entity, State state);
        
        Task<T> FindAsync(int entityId);

        Task SaveChangesAsync();

        Task CreateMigration(string migrationName);
        
        Task<List<T>> GetDbSet<T>(List<List<T>> dbSets);

        Task<DbSet<T>> Set();
    }
}