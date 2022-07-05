using Microsoft.AspNetCore.Mvc;
using ScoreManager.Data;
using ScoreManager.Entities;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : CrudBaseController<Category>
    {
        public CategoryController(ILogger<CategoryController> logger, ICategoryDAL crud) : base(logger, crud)
        {
        }
    }
}