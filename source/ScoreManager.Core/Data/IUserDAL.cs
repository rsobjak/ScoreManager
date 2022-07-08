using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public interface IUserDAL : ICrudBase<User>
    {
        public Task<User?> GetByLoginAsync(string login);
    }
}