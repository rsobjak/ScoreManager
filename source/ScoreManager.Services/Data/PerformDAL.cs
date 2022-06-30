using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class PerformDAL : CrudBase<Perform>, IPerform
    {
        public PerformDAL(ILogger<PerformDAL> logger, ApplicationDbContext db) : base(logger, db)
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
            return await getQuery().FirstOrDefaultAsync(w => w.Id == id);
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