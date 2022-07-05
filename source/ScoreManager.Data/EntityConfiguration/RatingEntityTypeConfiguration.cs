using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreManager.Entities;

namespace ScoreManager.EntityConfiguration
{
    internal class RatingEntityTypeConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.HasOne(x => x.Perform).WithMany(x => x.ra);
            //builder.HasIndex(b => new { b.PrimaryCandidate.Id, Id2 = b.SecondaryCandidate.Id, b.CategoryId }).IsUnique();
        }
    }
}