using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class RatingDalService : CrudBase<Rating>, IRatingDAL
    {
        public RatingDalService(ILogger<RatingDalService> logger, IHttpContextAccessor httpContextAccessor, ApplicationDbContext db) : base(logger, httpContextAccessor, db)
        {
        }
    }
}