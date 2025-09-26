using Backend.Domains.Quiz.Dto.Question;

namespace Backend.Domains.Quiz.Mapping.Question;

public interface IQuestionMapper
{
    GetQuestionDto GetQuestionDto(Entity.Question question);
    Entity.Question CreateQuestionDtoToQuestion(CreateQuestionDto createQuestionDto);
    void UpdateQuestionDto(Entity.Question question, UpdateQuestionDto updateQuestionDto);
}