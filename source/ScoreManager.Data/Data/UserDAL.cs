using ScoreManager.Entities;
using Microsoft.Extensions.Logging;

namespace ScoreManager.Data
{
    public class UserDAL : CrudBase<User>, IUser
    {
        public UserDAL(ILogger<UserDAL> logger, ScoreManagerDbContext db) : base(logger, db)
        {
        }
    }
}