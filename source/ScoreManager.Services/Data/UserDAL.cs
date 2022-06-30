using ScoreManager.Entities;
using Microsoft.Extensions.Logging;

namespace ScoreManager.Data
{
    public class UserDAL : CrudBase<User>, IUser
    {
        public UserDAL(ILogger<UserDAL> logger, ApplicationDbContext db) : base(logger, db)
        {
        }
    }
}