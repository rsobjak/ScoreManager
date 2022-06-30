using ScoreManager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ScoreManager.Data
{
    public class ScoreManagerDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        private readonly ILoggerFactory _loggerFactory;

        public ScoreManagerDbContext(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(_loggerFactory);
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<User> Users { get; set; }
    }
}