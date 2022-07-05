using ScoreManager.Entities;
using Microsoft.Extensions.Logging;

namespace ScoreManager.Data
{
    public class UserDalService : CrudBase<User>, IUserDAL
    {
        public UserDalService(ILogger<UserDalService> logger, ApplicationDbContext db) : base(logger, db)
        {
        }
    }
}