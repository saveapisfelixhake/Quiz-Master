using Backend.Domains.Quiz.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Domains.Quiz.Persistence.Configuration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");
        builder.HasKey(q => q.Id);
        builder
            .HasMany(p => p.Answers)
            .WithOne()
            .HasForeignKey(q => q.QuestionId);
        
        builder.Property(q => q.QuestionText).HasColumnType("longtext").IsRequired();
        builder.Property(q => q.MultipleAnswers).IsRequired();
        builder.Property(q => q.HasTextInput).IsRequired();
    }
}