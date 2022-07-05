namespace ScoreManager.Services
{
    public interface IOrderingService
    {
        Task ReorderCategories(int categoryId, int order);

        Task ReorderPerforms(int performId, int order);
    }
}