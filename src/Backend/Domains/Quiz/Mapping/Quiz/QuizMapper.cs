using Backend.Domains.Quiz.Dto.Quiz;

namespace Backend.Domains.Quiz.Mapping.Quiz;

public class QuizMapper : IQuizMapper
{
    public GetQuizDto MapQuizToGetQuizDto(Entity.Quiz quiz)
    {
        return new GetQuizDto
        {
            Id = quiz.Id,
            Name = quiz.Name,
            Description = quiz.Description,
            StartDate = quiz.StartDate,
            EndDate = quiz.EndDate,
            IsActive = quiz.IsActive,
            Questions = quiz.Questions,
            Bars = quiz.Bars,
        };
    }

    public Entity.Quiz MapCreateQuizDtoToQuiz(CreateQuizDto createQuizDto)
    {
        return new Entity.Quiz
        {
            Id = Guid.NewGuid(),
            Name = createQuizDto.Name,
            StartDate = createQuizDto.StartDate,
            EndDate = createQuizDto.EndDate,
            IsActive = createQuizDto.IsActive,
            Questions = []
        };
    }

    public void MapUpdateQuizDtoToQuiz(UpdateQuizDto updateQuizDto, Entity.Quiz quiz)
    {
        quiz.Name = updateQuizDto.Name;
        quiz.Description = updateQuizDto.Description;
        quiz.StartDate = updateQuizDto.StartDate;
        quiz.EndDate = updateQuizDto.EndDate;
        quiz.IsActive = updateQuizDto.IsActive;
        quiz.Bars = updateQuizDto.Bars;
    }
}