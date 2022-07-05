using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class CandidateDalService : CrudBase<Candidate>, ICandidateDAL
    {
        public CandidateDalService(ILogger<CandidateDalService> logger, ApplicationDbContext db) : base(logger, db)
        {
        }
    }
}