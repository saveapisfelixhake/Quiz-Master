using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Domains.Quiz.Persistence.Configuration;

public class QuizConfiguration : IEntityTypeConfiguration<Entity.Quiz>
{
    public void Configure(EntityTypeBuilder<Entity.Quiz> builder)
    {
        builder.ToTable("Quizzes");
        builder.HasKey(q => q.Id);
        builder
            .HasMany(p => p.Questions)
            .WithOne()
            .HasForeignKey(q => q.QuizId);

        builder.HasMany(q => q.Bars)
            .WithMany(b => b.Quizzes);
        
        builder.Property(q => q.Name).HasMaxLength(20).IsRequired();
        builder.Property(q => q.Description).HasColumnType("longtext").IsRequired();
        builder.Property(q => q.StartDate).IsRequired();
        builder.Property(q => q.QuizState).IsRequired();
        builder.Property(q => q.EndDate).IsRequired();
        builder.Property(q => q.IsActive).IsRequired();
    }
}