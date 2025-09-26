namespace Backend.Domains.Quiz.Entity;

public class Question
{
    public Guid Id { get; init; }
    
    public Guid QuizId { get; set; }
    public string Name { get; set; }
    
    public string QuestionText { get; set; }
    
    public bool MultipleAnswers { get; set; }
    
    public bool HasTextInput { get; set; }
    
    public List<Answer> Answers { get; set; }
}