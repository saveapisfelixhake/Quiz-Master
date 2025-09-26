namespace Backend.Domains.Quiz.Dto.Answer;

public class GetAnswerDto
{
    public Guid Id { get; init; }
    
    public Guid QuestionId { get; set; }
    
    public string Name { get; set; }
    
    public bool IsCorrect { get; set; }
}