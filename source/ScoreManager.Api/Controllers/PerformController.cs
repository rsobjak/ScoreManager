using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreManager.Data;
using ScoreManager.Dto.Request;
using ScoreManager.Entities;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformController : ControllerBase
    {
        private readonly ILogger<PerformController> _logger;
        private readonly IPerformDAL _crud;
        private readonly ICategoryDAL _categoryCrud;
        private readonly ICandidateDAL _candidateCrud;
        private readonly IUserDAL _userCrud;
        private readonly IRatingDAL _ratingCrud;

        public PerformController(ILogger<PerformController> logger, IPerformDAL crud, ICategoryDAL categoryCrud, ICandidateDAL candidateCrud, IUserDAL userCrud, IRatingDAL ratingCrud)
        {
            _logger = logger;
            _crud = crud;
            _categoryCrud = categoryCrud;
            _candidateCrud = candidateCrud;
            _ratingCrud = ratingCrud;
            _userCrud = userCrud;
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
        public async Task<IActionResult> Post(InsertPerformDto dto)
        {
            try
            {
                var category = await _categoryCrud.GetByIdAsync(dto.CategoryId);
                if (category == null)
                    return BadRequest("Category not found");
                var primaryCandidate = await _candidateCrud.GetByIdAsync(dto.PrimaryCandidateId);

                if (primaryCandidate == null)
                    return BadRequest("Primary Candidate not found");

                var secondaryCandidate = await _candidateCrud.GetByIdAsync(dto.SecondaryCandidateId);
                if (primaryCandidate == null && dto.SecondaryCandidateId > 0)
                    return BadRequest("Secondary Candidate not found");

                var entity = new Perform
                {
                    Category = category,
                    PrimaryCandidate = primaryCandidate,
                    SecondaryCandidate = secondaryCandidate
                };
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
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAll[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IEnumerable<Perform>> Get()
        {
            return await _crud.GetAllAsync();
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
        public async Task<Perform> Get(int id)
        {
            return await _crud.GetByIdAsync(id);
        }

        /// <summary>
        /// Add Rate
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("rate", Name = "AddRate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> PostRate(InsertRateDto dto)
        {
            try
            {
                var user = await _userCrud.GetByIdAsync(dto.UserId);
                if (user == null)
                    return BadRequest("User not found");

                var perform = await _crud.GetByIdAsync(dto.PerformId);
                if (perform == null)
                    return BadRequest("Perform not found");

                var entity = new Rating
                {
                    User = user,
                    Perform = perform,
                    Rate = dto.Rate,
                };
                await _ratingCrud.InsertAsync(entity);
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
    }
}