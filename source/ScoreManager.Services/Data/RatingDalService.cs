using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class RatingDalService : CrudBase<Rating>, IRatingDAL
    {
        public RatingDalService(ILogger<RatingDalService> logger, ApplicationDbContext db) : base(logger, db)
        {
        }
    }
}