using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ORMPractice
{
    public class Repository<T> where T : class
    {
        private readonly AppContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public List<T> GetAll()
        {
            try
            {
                if(_dbSet.Count() > 0)
                {
                    return _dbSet.ToList();
                }
                throw new Exception("The list's length is 0.");
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during returning the list of entities: {ex.Message}");
            }
        }

        public T Get(long id)
        {
            try
            {
                var entity = _dbSet.Find(id); ;

                if (entity != null)
                {
                    return entity;
                }
                throw new Exception("No entities with such id found.");
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during getting the entity: {ex.Message}");
            }
        }

        public void Add(T entity)
        {

            try
            {
                _dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during adding the entity: {ex.Message}");
            }
        }

        public void Update(int entityId, T entity)
        {
            try
            {
                var baseEntity = Get(entityId);
                _context.Entry(baseEntity).CurrentValues.SetValues(entity);


            }
            catch(Exception ex)
            {
                throw new Exception($"There was a problem during updating the entity: {ex.Message}");
            }
        }

        public List<T> Sort(string column, bool order)
        {
            try
            {
                var list = GetAll();
                if (order)
                {
                    return list.OrderBy(s => typeof(T).GetProperty(column).GetValue(s).ToString()).ToList();
                }
                else
                {
                    return list.OrderByDescending(s => typeof(T).GetProperty(column).GetValue(s).ToString()).ToList();
                }
            }
            catch( Exception ex)
            {
                throw new Exception($"There was a problem during sorting the DBSet: {ex.Message}");
            }
        }

        public List<T> Pagination(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var items = GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();



                return items;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during pagination: {ex.Message}");
            }
        }

        public void Delete(long id)
        {
            try
            {
                var entity = Get(id);

                if (entity != null)
                {
                    _dbSet.Remove(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during deleting the entity: {ex.Message}");
            }
        }


        public T Search(Func<T, bool> predicate)
        {
            try
            {
                var entity = _dbSet.FirstOrDefault(predicate);

                if (entity != null)
                {
                    return entity;
                }
                throw new Exception("No entities by such predicate found");
            }
            catch (Exception ex)
            {
                throw new Exception($"There was a problem during searching the entity: {ex.Message}");
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
