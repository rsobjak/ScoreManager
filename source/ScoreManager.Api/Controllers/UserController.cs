using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreManager.Data;
using ScoreManager.Entities;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class UserController : ControllerBase
    {
        protected readonly ILogger<object> _logger;
        protected readonly IUserDAL _crud;

        public UserController(ILogger<object> logger, IUserDAL crud)
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
        public async Task<ActionResult<IEnumerable<User>>> Get()
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
        public async Task<ActionResult<User>> Get(int id)
        {
            if (id <= 0) return BadRequest("Invalid parameter 'id'");

            var data = await _crud.GetByIdAsync(id);

            if (data == null) return NoContent();
            return Ok(data);
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{login}", Name = "Get[controller]ByLogin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        public async Task<ActionResult<User>> Get(string login)
        {
            var data = await _crud.GetByLoginAsync(login);

            if (data == null) return NoContent();
            return Ok(data);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost(Name = "Add[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        public async Task<ActionResult<User>> Post(User entity)
        {
            try
            {
                await _crud.InsertAsync(entity);
                return CreatedAtAction(nameof(this.Get), new { id = entity.Id }, entity);
            }
            catch (DbUpdateException e)
            {
                var message = e.InnerException != null ? e.InnerException.Message : e.Message;
                return BadRequest(message);
            }
            catch
            {
                throw;
            }
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
        public async Task<ActionResult<User>> Put(int id, User entity)
        {
            try
            {
                if (id != entity.Id)
                    return BadRequest("Please specify 'id' parameter same 'entity.Id'");

                await _crud.UpdateAsync(entity);
                return Ok();
            }
            catch (DbUpdateException e)
            {
                var message = e.InnerException != null ? e.InnerException.Message : e.Message;
                return BadRequest(message);
            }
            catch
            {
                throw;
            }
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _crud.RemoveAsync(id);
                return Ok();
            }
            catch (DbUpdateException e)
            {
                var message = e.InnerException != null ? e.InnerException.Message : e.Message;
                return BadRequest(message);
            }
            catch
            {
                throw;
            }
        }
    }
}