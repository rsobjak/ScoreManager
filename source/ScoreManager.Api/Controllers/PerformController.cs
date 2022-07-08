using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreManager.Data;
using ScoreManager.Dto.Request;
using ScoreManager.Dto.Response;
using ScoreManager.Entities;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PerformController : ControllerBase
    {
        private readonly ILogger<PerformController> _logger;
        private readonly IPerformDAL _crud;
        private readonly ICategoryDAL _categoryCrud;
        private readonly ICandidateDAL _candidateCrud;
        private readonly IUserDAL _userCrud;
        private readonly IRatingDAL _ratingCrud;
        private readonly IMapper _mapper;

        public PerformController(ILogger<PerformController> logger, IPerformDAL crud, ICategoryDAL categoryCrud, ICandidateDAL candidateCrud, IUserDAL userCrud, IRatingDAL ratingCrud, IMapper mapper)
        {
            _logger = logger;
            _crud = crud;
            _categoryCrud = categoryCrud;
            _candidateCrud = candidateCrud;
            _ratingCrud = ratingCrud;
            _userCrud = userCrud;
            _mapper = mapper;
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
        public async Task<ActionResult<Perform>> Post(InsertPerformDto dto)
        {
            try
            {
                var entity = _mapper.Map<Perform>(dto);

                var category = await _categoryCrud.GetByIdAsync(dto.CategoryId);
                if (category == null)
                    return BadRequest("Category not found");
                var primaryCandidate = await _candidateCrud.GetByIdAsync(dto.PrimaryCandidateId);

                if (primaryCandidate == null)
                    return BadRequest("Primary Candidate not found");

                var secondaryCandidate = await _candidateCrud.GetByIdAsync(dto.SecondaryCandidateId);
                if (primaryCandidate == null && dto.SecondaryCandidateId > 0)
                    return BadRequest("Secondary Candidate not found");

                var order = await _crud.GetMaxOrderByCategory(dto.CategoryId);

                entity.Category = category;
                entity.PrimaryCandidate = primaryCandidate;
                entity.SecondaryCandidate = secondaryCandidate;
                entity.Order = order + 1;

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
        /// Get All
        /// </summary>
        /// <param name="onlyCreatedByMe"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetAll[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        public async Task<ActionResult<IEnumerable<Perform>>> Get(bool onlyCreatedByMe, int? categoryId)
        {
            if (!onlyCreatedByMe && !(User.IsInRole("admin") || User.IsInRole("judge")))
                return Forbid();

            if (categoryId.HasValue && !(User.IsInRole("admin") || User.IsInRole("judge")))
                return Forbid();

            return Ok(await _crud.GetAllAsync(onlyCreatedByMe, categoryId));
        }

        /// <summary>
        /// Get Pendings
        /// </summary>
        /// <returns></returns>
        [HttpGet("pendings", Name = "GetPending[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        [Authorize(Roles = "admin,judge")]
        public async Task<ActionResult<IEnumerable<PerformDto>>> GetPendings()
        {
            var pendings = await _crud.GetPendingsAsync();
            var mapped = _mapper.Map<IEnumerable<PerformDto>>(pendings);
            return Ok(mapped);
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
        public async Task<ActionResult<Perform>> Get(int id)
        {
            var isCreatedByUser = await _crud.IsCreatedByUser(id, User.Identity.Name);
            if (!(User.IsInRole("admin") || User.IsInRole("judge")) && !isCreatedByUser)
                return Forbid();
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
        [Authorize(Roles = "admin,judge")]
        public async Task<ActionResult<Rating>> PostRate(InsertRateDto dto)
        {
            try
            {
                var entity = _mapper.Map<Rating>(dto);
                var user = await _userCrud.GetByLoginAsync(User.Identity.Name);
                if (user == null)
                    user = await _userCrud.InsertAsync(new User { Login = User.Identity.Name, IsRater = true });

                var perform = await _crud.GetByIdAsync(dto.PerformId);
                if (perform == null)
                    return BadRequest("Perform not found");

                if (perform.Ratings.Any(a => a.User == user))
                    return BadRequest("Usuário já forneceu a nota para esta apresentação");

                //TODO: Business rule parameter
                if (dto.Rate < 5 || dto.Rate > 10)
                    return BadRequest("Invalid Rate. Must be between 5.0 and 10.0");

                entity.User = user;
                entity.Perform = perform;

                await _ratingCrud.InsertAsync(entity);

                await _crud.CalculateScore(dto.PerformId);

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
        /// Delete Rate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("rate", Name = "DeleteRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        [Authorize(Roles = "admin,judge")]
        public async Task<ActionResult> DeleteRate(int id)
        {
            var rating = await _ratingCrud.GetByIdAsync(id);
            if (rating == null)
                return NotFound();

            await _ratingCrud.RemoveAsync(id);

            await _crud.CalculateScore(rating.Perform.Id);

            return Ok();
        }

        /// <summary>
        /// Change Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPatch(Name = "ChangeStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResult))]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateStatusRate(int id, [FromForm] PerformStatus status)
        {
            var perform = await _crud.GetByIdAsync(id);
            if (perform == null)
                return NotFound();

            perform.Status = status;
            perform = await _crud.UpdateAsync(perform);

            return Ok(perform);
        }
    }
}