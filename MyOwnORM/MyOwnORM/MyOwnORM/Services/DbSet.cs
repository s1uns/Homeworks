using MyOwnORM.Interfaces;
using MyOwnORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnORM.Services
{
    public class DbSet<T> : IDbSet<T> where T : BaseEntity
    {
        private List<T> _entities;

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                _entities.Add(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during adding the entity to DbSet: {ex.Message}");
            }
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            try
            {
                foreach(var entity in entities)
                {
                    _entities.Add(entity);
                }

                return _entities;
            }
            catch(Exception ex)
            {
                throw new Exception($"There was a problem during adding the entities to DbSet: {ex.Message}");
            }
        }

        public async Task<bool> Any()
        {
            try
            {
                return _entities.Any();
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during checking whether theere is any entity in the DbSet: {ex.Message}");
            }
        }

        public async Task<int> Count()
        {
            try
            {
                return _entities.Count;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during getting the DbSet's length: {ex.Message}");
            }
        }

        public async Task<T> FindAsync(int id)
        {
            try
            {
                var entity = _entities.Where(x => x.Id.Equals(id)).FirstOrDefault();

                if(entity == null)
                {
                    throw new Exception("Entity is not found.");
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during finding the entity in the DbSet: {ex.Message}");
            }
        }

        public async Task<T> FirstOrDefault()
        {
            try
            {
                var entity = _entities.FirstOrDefault();

                if (entity == null)
                {
                    throw new Exception("No entities in the DbSet.");
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during finding any entities in the DbSet: {ex.Message}");
            }
        }

        public async Task RemoveAsync(T entity)
        {
            try
            {
                _entities.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during deleting the entity from the DbSet: {ex.Message}");
            }
        }

        public async Task<List<T>> ToList()
        {
            try
            {
                return _entities;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during converting the DbSet to a list: {ex.Message}");
            }
        }

        public async Task<IQueryable<T>> Where(Func<T, bool> predicate)
        {
            try
            {
                return _entities.Where(predicate).AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during filtering the DbSet: {ex.Message}");
            }
        }
    }
}