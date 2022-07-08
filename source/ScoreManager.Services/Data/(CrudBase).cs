using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ScoreManager.Data
{
    public abstract class CrudBase<T> : ICrudBase<T> where T : EntityBase

    {
        protected DateTime time;
        protected readonly ApplicationDbContext _db;
        protected readonly ILogger<object> _logger;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public CrudBase(ILogger<object> logger, IHttpContextAccessor httpContextAccessor, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            time = DateTime.Now;
            var data = await _db.Set<T>()
                .ToListAsync();
            var elapsed = DateTime.Now.Subtract(time).TotalMilliseconds;
            _logger.LogInformation("Elapsed {elapsed} ms", elapsed);
            return data;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _db.Set<T>()
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public virtual async Task RemoveAsync(int id)
        {
            var entity = await this._db.Set<T>().FindAsync(id);
            if (entity != null)
                _db.Remove(entity);
            else
                throw new ArgumentException($"The entity of type {typeof(T).Name} not found", nameof(id));
            await _db.SaveChangesAsync();
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}