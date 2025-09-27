namespace Backend.Domains.Quiz.Entity;

public class Answer
{
    public Guid Id { get; init; }
    
    public Guid QuestionId { get; set; }
    
    public string Name { get; set; }
    
    public bool IsCorrect { get; set; }
}