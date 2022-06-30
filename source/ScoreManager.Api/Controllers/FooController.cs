using ScoreManager.Data;
using ScoreManager.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class FooController<T> : ControllerBase where T : Entity
    {
        protected readonly ILogger<object> _logger;
        protected readonly ICrudBase<T> _crud;

        public FooController(ILogger<object> logger, ICrudBase<T> crud)
        {
            _logger = logger;
            _crud = crud;
        }

        /// <summary>
        /// Get all [controller]
        /// </summary>
        /// <returns></returns>
        [HttpGet("asdf/", Name = "FoooBar[controller]")]
        public async Task<IEnumerable<T>> GetFoo()
        {
            return await _crud.GetAllAsync();
        }

    }
}