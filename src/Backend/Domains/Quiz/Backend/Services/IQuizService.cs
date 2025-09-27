using Backend.Domains.Quiz.Dto.Quiz;

namespace Backend.Domains.Quiz.Backend.Services;

public interface IQuizService
{
    public Guid Create(CreateQuizDto dto);
    public Guid? Update(Guid quizId, UpdateQuizDto dto);
    public GetQuizDto? Get(Guid quizId);
    public List<GetQuizDto>? All();
    public bool Delete(Guid quizId);
}