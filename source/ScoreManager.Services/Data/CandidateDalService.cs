using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class CandidateDalService : CrudBase<Candidate>, ICandidateDAL
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserDAL _userDal;

        public CandidateDalService(ILogger<CandidateDalService> logger, ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, IUserDAL userDal) : base(logger, httpContextAccessor, db)
        {
            _httpContextAccessor = httpContextAccessor;
            _userDal = userDal;
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync(bool onlyCreatedByMe)
        {
            User? user = null;
            if (onlyCreatedByMe)
                user = await _userDal.GetByLoginAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            IQueryable<Candidate> query = _db.Set<Candidate>();

            if (onlyCreatedByMe)
                query = query.Where(w => w.User == user);

            return await query.ToListAsync();
        }
    }
}