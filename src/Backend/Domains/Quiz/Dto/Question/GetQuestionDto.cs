namespace Backend.Domains.Quiz.Dto.Question;

public class GetQuestionDto
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public string Name { get; set; }
    
    public string QuestionText { get; set; }
    
    public bool MultipleAnswers { get; set; }
    
    public bool HasTextInput { get; set; }
    
    public List<Entity.Answer> Answers { get; set; }
}