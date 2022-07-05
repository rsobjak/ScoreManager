using Microsoft.AspNetCore.Mvc;
using ScoreManager.Data;
using ScoreManager.Entities;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : CrudBaseController<Candidate>
    {
        public CandidateController(ILogger<CandidateController> logger, ICandidateDAL dal) : base(logger, dal)
        {
        }
    }
}