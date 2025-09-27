namespace Backend.Domains.Quiz.Dto.Answer;

public class UpdateAnswerDto
{
    public Guid QuestionId { get; set; }
    public string Name { get; set; }
    public bool IsCorrect { get; set; }
}