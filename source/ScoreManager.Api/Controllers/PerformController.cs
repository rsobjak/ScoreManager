using Microsoft.AspNetCore.Mvc;
using ScoreManager.Data;
using ScoreManager.Entities;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformController : ControllerBase
    {
        private readonly ILogger<PerformController> _logger;
        private readonly IPerform _crud;
        private readonly ICategory _categoryCrud;
        private readonly ICandidate _candidateCrud;

        public PerformController(ILogger<PerformController> logger, IPerform crud, ICategory categoryCrud, ICandidate candidateCrud)
        {
            _logger = logger;
            _crud = crud;
            _categoryCrud = categoryCrud;
            _candidateCrud = candidateCrud;
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost(Name = "Add[controller]")]
        public async Task Post(InsertPerformDto dto)
        {
            var category = await _categoryCrud.GetByIdAsync(dto.CategoryId);
            var primaryCandidate = await _candidateCrud.GetByIdAsync(dto.PrimaryCandidateId);
            var secondaryCandidate = await _candidateCrud.GetByIdAsync(dto.SecondaryCandidateId);
            var entity = new Perform
            {
                Category = category,
                PrimaryCandidate = primaryCandidate,
                SecondaryCandidate = secondaryCandidate,
                Score = 0
            };
            await _crud.InsertAsync(entity);
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAll[controller]")]
        public async Task<IEnumerable<Perform>> Get()
        {
            return await _crud.GetAllAsync();
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get[controller]ById")]
        public async Task<Perform> Get(int id)
        {
            return await _crud.GetByIdAsync(id);
        }
    }

    public class InsertPerformDto
    {
        public int InsertUserId { get; set; }
        public int CategoryId { get; set; }
        public int PrimaryCandidateId { get; set; }
        public int SecondaryCandidateId { get; set; }
    }
}