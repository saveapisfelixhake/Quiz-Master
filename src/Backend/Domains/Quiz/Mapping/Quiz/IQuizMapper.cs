using Backend.Domains.Quiz.Dto.Quiz;

namespace Backend.Domains.Quiz.Mapping.Quiz;

public interface IQuizMapper
{
    GetQuizDto MapQuizToGetQuizDto(Entity.Quiz quiz);
    Entity.Quiz MapCreateQuizDtoToQuiz(CreateQuizDto createQuizDto);
    void MapUpdateQuizDtoToQuiz(UpdateQuizDto updateQuizDto,  Entity.Quiz quiz);
    
}