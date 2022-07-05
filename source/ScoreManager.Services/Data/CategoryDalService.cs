using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class CategoryDalService : CrudBase<Category>, ICategoryDAL
    {
        public CategoryDalService(ILogger<CategoryDalService> logger, ApplicationDbContext db) : base(logger, db)
        {
        }
    }
}