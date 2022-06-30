using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class CandidateDAL : CrudBase<Candidate>, ICandidate
    {
        public CandidateDAL(ILogger<CandidateDAL> logger, ApplicationDbContext db) : base(logger, db)
        {
        }
    }
}