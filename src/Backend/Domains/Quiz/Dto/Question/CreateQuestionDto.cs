namespace Backend.Domains.Quiz.Dto.Question;

public class CreateQuestionDto
{
    public string Name { get; set; }
    
    public string QuestionText { get; set; }
    
    public bool MultipleAnswers { get; set; }
    
    public bool HasTextInput { get; set; }
    
    public bool IsAnswered { get; set; }
}