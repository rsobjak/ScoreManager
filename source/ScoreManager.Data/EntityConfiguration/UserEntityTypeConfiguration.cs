using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreManager.Entities;

namespace ScoreManager.EntityConfiguration
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .Property(b => b.Login)
                .HasMaxLength(255)
                .IsRequired()
                ;
            builder.HasIndex(b => b.Login).IsUnique();
        }
    }
}