using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public interface IPerformDAL : ICrudBase<Perform>
    {
        Task<IEnumerable<Perform>> GetAllAsync(bool onlyCreatedByMe, int? categoryId);
        Task<IQueryable<Perform>> GetPendingsAsync();

        Task<bool> IsCreatedByUser(int id, string userLogin);

        Task<int> GetMaxOrderByCategory(int categoryId);

        Task CalculateScore(int id);
    }
}