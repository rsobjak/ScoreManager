using Microsoft.AspNetCore.Mvc;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class CrudBaseController<T> : ControllerBase where T : Entity
    {
        protected readonly ILogger<object> _logger;
        protected readonly ICrudBase<T> _crud;

        public CrudBaseController(ILogger<object> logger, ICrudBase<T> crud)
        {
            _logger = logger;
            _crud = crud;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAll[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            return Ok(await _crud.GetAllAsync());
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get[controller]ById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        public async Task<ActionResult<T>> Get(int id)
        {
            if (id <= 0) return BadRequest("Invalid parameter 'id'");

            var data = await _crud.GetByIdAsync(id);

            if (data == null) return NoContent();
            return Ok(data);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost(Name = "Add[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Post(T entity)
        {
            await _crud.InsertAsync(entity);
            return Ok();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "Update[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Put(int id, T entity)
        {
            await _crud.UpdateAsync(id, entity);
            return Ok();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete(Name = "Delete[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Delete(int id)
        {
            await _crud.RemoveAsync(id);
            return Problem();
        }
    }
}