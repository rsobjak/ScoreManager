using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class UserDalService : IUserDAL
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _db;
        private DateTime time;

        public UserDalService(ILogger<UserDalService> logger, IHttpContextAccessor httpContextAccessor, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public virtual async Task<IEnumerable<User>> GetAllAsync()
        {
            time = DateTime.Now;
            var data = await _db.Set<User>()
                .ToListAsync();
            var elapsed = DateTime.Now.Subtract(time).TotalMilliseconds;
            _logger.LogInformation("Elapsed {elapsed} ms", elapsed);
            return data;
        }

        public virtual async Task<User?> GetByIdAsync(int id)
        {
            return await _db.Set<User>()
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _db.Set<User>()
               .FirstOrDefaultAsync(w => w.Login == login);
        }

        public virtual async Task<User> InsertAsync(User entity)
        {
            await _db.Set<User>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public virtual async Task RemoveAsync(int id)
        {
            var entity = await this._db.Set<User>().FindAsync(id);
            if (entity != null)
                _db.Remove(entity);
            else
                throw new ArgumentException($"The entity of type {typeof(User).Name} not found", nameof(id));
            await _db.SaveChangesAsync();
        }

        public virtual async Task<User> UpdateAsync(User entity)
        {
            _db.Set<User>().Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}