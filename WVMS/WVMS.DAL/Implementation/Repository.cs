using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WVMS.DAL.Interfaces;

namespace WVMS.DAL.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private bool disposedValue = false;
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(null, nameof(context));
            _dbSet = _dbContext.Set<T>();
        }
        public T Add(T obj)
        {
            try
            {
                _dbSet.Add(obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<T> AddAsync(T obj)
        {
            Add(obj);
            await SaveAsync();
            return obj;
        }

        public void AddRange(IEnumerable<T> records)
        {
            try
            {
                _dbSet.AddRange(records);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddRangeAsync(IEnumerable<T> records)
        {
            AddRange(records);
            await SaveAsync();
        }

        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null) return _dbSet.Any();
            return _dbSet.Any(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null) return await _dbSet.AnyAsync();
            return await _dbSet.AnyAsync(predicate);
        }

        public long Count(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                if (predicate == null)
                    return _dbSet.LongCount();
                return _dbSet.LongCount(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                if (predicate == null)
                    return await _dbSet.LongCountAsync();
                return await _dbSet.LongCountAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var obj = GetSingleBy(predicate);
                if (obj != null)
                {
                    _dbSet.Remove(obj);
                    return true;
                }
                else
                    throw new Exception($"object does not exist");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(T obj)
        {
            try
            {
                _dbSet.Remove(obj);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T obj)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRange(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRange(IEnumerable<T> records)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<T> records)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params string[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, params string[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, params string[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public T GetSingleBy(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetSingleByAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool tracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetSingleByAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T> LastAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> SaveAsync()
        {
            try
            {
                return _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<decimal> SumAsync(Expression<Func<T, decimal>> predicate)
        {
            return await _dbSet.SumAsync(predicate);
        }

        public async Task<int> SumAsync(Expression<Func<T, int>> predicate)
        {
            return await _dbSet.SumAsync(predicate);
        }

        public async Task<long> SumAsync(Expression<Func<T, long>> predicate)
        {
            return await _dbSet.SumAsync(predicate);
        }

        public T Update(T obj)
        {
            try
            {
                _dbSet.Attach(obj);
                _dbContext.Entry<T>(obj).State = EntityState.Modified;
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> UpdateAsync(T obj)
        {
            Update(obj);
            await SaveAsync();
            return obj;
        }

        public void UpdateRange(IEnumerable<T> records)
        {
            try
            {
                _dbSet.UpdateRange(records);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<T> records)
        {
            UpdateRange(records);
            await SaveAsync();
        }
    }
}
