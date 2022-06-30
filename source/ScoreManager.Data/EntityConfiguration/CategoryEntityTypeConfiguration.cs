using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreManager.Entities;

namespace ScoreManager.EntityConfiguration
{
    internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(b => b.Name)
                .HasMaxLength(255)
                .IsRequired()
                ;
            builder.HasIndex(b => b.Name).IsUnique();
        }
    }
}