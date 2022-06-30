using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class CategoryDAL : CrudBase<Category>, ICategory
    {
        public CategoryDAL(ILogger<CategoryDAL> logger, ApplicationDbContext db) : base(logger, db)
        {
        }
    }
}