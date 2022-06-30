using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ScoreManager.Data
{
    public class CrudBase<T> : ICrudBase<T> where T : Entity

    {
        private DateTime time;
        private readonly ScoreManagerDbContext _db;
        private readonly ILogger<object> _logger;

        public CrudBase(ILogger<object> logger, ScoreManagerDbContext db)
        {
            this._db = db;
            this._logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            time = DateTime.Now;
            var data = await _db.Set<T>().Where(w => !w.IsDeleted).ToListAsync();
            var elapsed = DateTime.Now.Subtract(time).TotalMilliseconds;
            _logger.LogInformation("Elapsed {elapsed} ms", elapsed);
            return data;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _db.Set<T>().Where(w => !w.IsDeleted).FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task InsertAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await this._db.Set<T>().FindAsync(id);
            if (entity != null)
                _db.Remove(entity);
            else
                throw new ArgumentException("The entity not found", nameof(id));
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}