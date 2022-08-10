using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreManager.Entities;

namespace ScoreManager.EntityConfiguration
{
    internal class CandidateEntityTypeConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .Property(b => b.Document)
                .HasMaxLength(20)
                .HasColumnType("nvarchar")
                .IsRequired()
                ;
            builder
                .Property(b => b.Name)
                .HasMaxLength(255)
                .HasColumnType("nvarchar")
                .IsRequired()
                ;
            builder.HasIndex(b => b.Document).IsUnique();
        }
    }
}