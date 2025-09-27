namespace Backend.Domains.Quiz.Dto.Question;

public class UpdateQuestionDto
{
    public Guid QuizId { get; set; }
    public string Name { get; set; }
    
    public string QuestionText { get; set; }
    
    public bool MultipleAnswers { get; set; }
    
    public bool HasTextInput { get; set; }
}