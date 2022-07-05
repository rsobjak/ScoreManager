using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScoreManager.Data;
using ScoreManager.Dto.Request;
using ScoreManager.Entities;

namespace ScoreManager.Services
{
    public class OrderingService : IOrderingService
    {
        private readonly ILogger<OrderingService> _logger;
        private readonly IPerformDAL _performCrud;
        private readonly ICategoryDAL _categoryCrud;
        private readonly ICandidateDAL _candidateCrud;
        private readonly IUserDAL _userCrud;
        private readonly IRatingDAL _ratingCrud;

        public OrderingService(ILogger<OrderingService> logger, IPerformDAL performCrud, ICategoryDAL categoryCrud, ICandidateDAL candidateCrud, IUserDAL userCrud, IRatingDAL ratingCrud)
        {
            _logger = logger;
            _performCrud = performCrud;
            _categoryCrud = categoryCrud;
            _candidateCrud = candidateCrud;
            _ratingCrud = ratingCrud;
            _userCrud = userCrud;
        }

        public Task ReorderCategories(int categoryId, int order)
        {
            throw new NotImplementedException();
        }

        public Task ReorderPerforms(int performId, int order)
        {
            throw new NotImplementedException();
        }
    }
}