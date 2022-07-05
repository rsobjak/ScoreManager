using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class PerformDalService : CrudBase<Perform>, IPerformDAL
    {
        public PerformDalService(ILogger<PerformDalService> logger, ApplicationDbContext db) : base(logger, db)
        {
        }

        public override async Task<IEnumerable<Perform>> GetAllAsync()
        {
            time = DateTime.Now;
            var data = await getQuery().ToListAsync();
            var elapsed = DateTime.Now.Subtract(time).TotalMilliseconds;
            _logger.LogInformation("Elapsed {elapsed} ms", elapsed);
            return data;
        }

        public override async Task<Perform> GetByIdAsync(int id)
        {
            return await getQuery()
                .Include(s => s.Ratings)
                .FirstAsync(w => w.Id == id);
        }

        private IQueryable<Perform> getQuery()
        {
            return _db.Set<Perform>()
                .Include(s => s.Category)
                .Include(s => s.PrimaryCandidate)
                .Include(s => s.SecondaryCandidate);
        }
    }
}