using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ScoreManager.EntityConfiguration;

namespace ScoreManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        private readonly ILoggerFactory _loggerFactory;

        public ApplicationDbContext(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(_loggerFactory);
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);
        }
    }
}