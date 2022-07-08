namespace ScoreManager
{
    public interface ICrudBase<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> InsertAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task RemoveAsync(int id);
    }
}