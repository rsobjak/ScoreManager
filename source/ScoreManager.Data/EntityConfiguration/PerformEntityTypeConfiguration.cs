using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreManager.Entities;

namespace ScoreManager.EntityConfiguration
{
    internal class PerformEntityTypeConfiguration : IEntityTypeConfiguration<Perform>
    {
        public void Configure(EntityTypeBuilder<Perform> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.HasOne(x => x.Category).WithMany(x => x.Performs);
            //builder.HasIndex(b => new { b.PrimaryCandidate.Id, Id2 = b.SecondaryCandidate.Id, b.CategoryId }).IsUnique();
        }
    }
}