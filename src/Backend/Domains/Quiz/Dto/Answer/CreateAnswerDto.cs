namespace Backend.Domains.Quiz.Dto.Answer;

public class CreateAnswerDto
{
    public Guid Id { get; init; }
    
    public string Name { get; set; }
    
    public bool IsCorrect { get; set; }
}