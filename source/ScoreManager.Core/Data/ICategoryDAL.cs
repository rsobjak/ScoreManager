using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public interface ICategoryDAL : ICrudBase<Category>
    {
        Task<int> GetMaxOrder();
    }
}