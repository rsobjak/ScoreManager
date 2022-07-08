using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreManager.Data;
using ScoreManager.Dto.Request;
using ScoreManager.Entities;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CandidateController : ControllerBase
    {
        protected readonly ILogger<object> _logger;
        protected readonly ICandidateDAL _crud;
        protected readonly IUserDAL _userCrud;
        protected readonly IMapper _mapper;

        public CandidateController(ILogger<object> logger, ICandidateDAL crud, IUserDAL userCrud, IMapper mapper)
        {
            _logger = logger;
            _crud = crud;
            _mapper = mapper;
            _userCrud = userCrud;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAll[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]

        public async Task<ActionResult<IEnumerable<Candidate>>> Get(bool onlyCreatedByMe = true)
        {
            if(!onlyCreatedByMe && !(User.IsInRole("admin") || User.IsInRole("judge")))
                return Forbid();
            return Ok(await _crud.GetAllAsync(onlyCreatedByMe));
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
        [Authorize(Roles = "admin,judge")]
        public async Task<ActionResult<Candidate>> Get(int id)
        {
            if (id <= 0) return BadRequest("Invalid parameter 'id'");

            var data = await _crud.GetByIdAsync(id);

            if (data == null) return NoContent();
            return Ok(data);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost(Name = "Add[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        public async Task<ActionResult<Candidate>> Post(InsertCandidateDto dto)
        {
            try
            {
                var entity = _mapper.Map<Candidate>(dto);

                var user = await _userCrud.GetByLoginAsync(User.Identity.Name);
                if (user == null)
                {
                    user = await _userCrud.InsertAsync(new User { Login = User.Identity.Name });
                }
                entity.User = user;

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
        public async Task<ActionResult<Candidate>> Put(int id, Candidate entity)
        {
            try
            {
                if (id != entity.Id)
                    return BadRequest("Please specify 'id' parameter same 'entity.Id'");

                var data = await _crud.GetByIdAsync(id);
                if (data.User.Login != User.Identity.Name)
                    return Forbid("Not authorized to this entity");

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