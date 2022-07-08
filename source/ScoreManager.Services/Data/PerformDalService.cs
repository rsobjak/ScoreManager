using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class PerformDalService : CrudBase<Perform>, IPerformDAL
    {
        private readonly IUserDAL _userDal;

        public PerformDalService(ILogger<PerformDalService> logger, IHttpContextAccessor httpContextAccessor, ApplicationDbContext db, IUserDAL userDal) : base(logger, httpContextAccessor, db)
        {
            _userDal = userDal;
        }

        public async Task CalculateScore(int id)
        {
            var perform = await GetByIdAsync(id);
            perform.Score = perform.Ratings.Average(a => a.Rate);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Perform>> GetAllAsync(bool onlyCreatedByMe, int? categoryId)
        {
            User? user = null;
            if (onlyCreatedByMe)
                user = await _userDal.GetByLoginAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            IQueryable<Perform> query = _db.Set<Perform>();

            if (onlyCreatedByMe)
                query = query.Where(w => w.PrimaryCandidate.User == user);

            if (categoryId.HasValue)
                query = query.Where(w => w.Category.Id == categoryId);

            return await query.ToListAsync();
        }

        public override async Task<Perform> GetByIdAsync(int id)
        {
            return await getQuery()
                .Include(s => s.Ratings)
                    .ThenInclude(s => s.User)
                .FirstAsync(w => w.Id == id);
        }

        public async Task<int> GetMaxOrderByCategory(int categoryId)
        {
            return (await _db.Set<Perform>().Where(w => w.Category.Id == categoryId).MaxAsync(s => s.Order)).GetValueOrDefault();
        }

        public async Task<IQueryable<Perform>> GetPendingsAsync()
        {
            User? user = await _userDal.GetByLoginAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            IQueryable<Perform> query = _db.Set<Perform>()
                .Include(i => i.Category)
                .Where(w =>
                    !w.Ratings.Any(a => a.User == user) &&
                    w.Status == PerformStatus.Confirmed ||
                    w.Status == PerformStatus.Performing
                );

            return query;
        }

        public async Task<bool> IsCreatedByUser(int id, string userLogin)
        {
            return await _db.Set<Perform>().FirstOrDefaultAsync(w => w.Id == id && w.PrimaryCandidate.User.Login == userLogin) != null;
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