using ScoreManager.Data;
using ScoreManager.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : CrudBaseController<User>
    {
        public UserController(ILogger<UserController> logger, IUserDAL dal) : base(logger, dal)
        {
        }
    }
}