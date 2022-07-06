namespace ScoreManager
{
    public interface ICrudBase<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task RemoveAsync(int id);
    }
}