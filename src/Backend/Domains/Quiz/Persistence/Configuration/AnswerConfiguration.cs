using Backend.Domains.Quiz.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Domains.Quiz.Persistence.Configuration;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable("Answers");
        builder.HasKey(q => q.Id);
        
        builder.Property(q => q.Name).HasColumnType("longtext").IsRequired();
        builder.Property(q => q.IsCorrect).IsRequired();
    }
}