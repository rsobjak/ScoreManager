using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreManager.Data;
using ScoreManager.Entities;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class UserController : CrudBaseController<User>
    {
        public UserController(ILogger<UserController> logger, IUserDAL dal) : base(logger, dal)
        {
        }
    }
}