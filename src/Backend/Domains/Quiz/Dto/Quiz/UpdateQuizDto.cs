using Backend.Domains.Bar.Domain.Models.Entities;

namespace Backend.Domains.Quiz.Dto.Quiz;

public class UpdateQuizDto
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public bool IsActive { get; set; }
    
    public List<BarEntity> Bars { get; set; }
}