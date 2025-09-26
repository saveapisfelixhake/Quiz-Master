using Backend.Domains.Quiz.Dto.Answer;

namespace Backend.Domains.Quiz.Mapping.Answer;

public class AnswerMapper : IAnswerMapper
{
    public GetAnswerDto AnswertoGetAnswerDto(Entity.Answer answer)
    {
        return new GetAnswerDto
        {
            Id = answer.Id,
            QuestionId = answer.QuestionId,
            Name = answer.Name,
            IsCorrect = answer.IsCorrect,
        };
    }

    public Entity.Answer MapCreateAnswerDtoToAnswer(CreateAnswerDto createAnswerDto)
    {
        return new Entity.Answer
        {
            Id = Guid.NewGuid(),
            Name = createAnswerDto.Name,
            IsCorrect = createAnswerDto.IsCorrect,
        };
    }

    public void UpdateAnswerDtoToAnswer(UpdateAnswerDto updateAnswerDto, Entity.Answer answer)
    {
        answer.QuestionId = updateAnswerDto.QuestionId;
        answer.Name = updateAnswerDto.Name;
        answer.IsCorrect = updateAnswerDto.IsCorrect;
    }
}