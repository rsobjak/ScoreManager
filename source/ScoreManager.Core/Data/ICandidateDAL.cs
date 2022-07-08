using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public interface ICandidateDAL : ICrudBase<Candidate>
    {
        Task<IEnumerable<Candidate>> GetAllAsync(bool onlyCreatedByMe);
    }
}