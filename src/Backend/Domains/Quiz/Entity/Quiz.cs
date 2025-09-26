using Backend.Domains.Bar.Domain.Models.Entities;

namespace Backend.Domains.Quiz.Entity;

public class Quiz
{
    public Guid Id { get; init; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public bool IsActive { get; set; }
    
    public List<Question> Questions { get; set; }
    
    public List<BarEntity> Bars { get; set; }
}