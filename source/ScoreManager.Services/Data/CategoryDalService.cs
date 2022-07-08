using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScoreManager.Entities;

namespace ScoreManager.Data
{
    public class CategoryDalService : CrudBase<Category>, ICategoryDAL
    {
        public CategoryDalService(ILogger<CategoryDalService> logger, IHttpContextAccessor httpContextAccessor, ApplicationDbContext db) : base(logger, httpContextAccessor,db)
        {
        }

        public async Task<int> GetMaxOrder()
        {
            return (await base._db.Set<Category>().MaxAsync(x => x.Order)).GetValueOrDefault();
        }
    }
}