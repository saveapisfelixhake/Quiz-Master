using Backend.Domains.Quiz.Entity.Enum;

namespace Backend.Domains.Quiz.Dto.Quiz;

public class CreateQuizDto
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public bool IsActive { get; set; }
    
    public QuizStateEnum QuizState { get; set; }

}