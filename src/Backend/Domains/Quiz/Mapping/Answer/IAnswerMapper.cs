using Backend.Domains.Quiz.Dto.Answer;

namespace Backend.Domains.Quiz.Mapping.Answer;

public interface IAnswerMapper
{
    GetAnswerDto AnswertoGetAnswerDto(Entity.Answer answer);
    Entity.Answer MapCreateAnswerDtoToAnswer(CreateAnswerDto createAnswerDto);
    void UpdateAnswerDtoToAnswer(UpdateAnswerDto updateAnswerDto, Entity.Answer answer);
}