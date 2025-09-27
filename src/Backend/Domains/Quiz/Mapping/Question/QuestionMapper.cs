using Backend.Domains.Quiz.Dto.Question;

namespace Backend.Domains.Quiz.Mapping.Question;

public class QuestionMapper : IQuestionMapper
{
    public GetQuestionDto GetQuestionDto(Entity.Question question)
    {
        return new GetQuestionDto
        {
            Id = question.Id,
            QuestionId = question.Id,
            Name = question.Name,
            QuestionText = question.QuestionText,
            MultipleAnswers = question.MultipleAnswers,
            HasTextInput = question.HasTextInput,
            IsAnswered = question.IsAnswered,
            Answers = question.Answers,
        };
    }

    public Entity.Question CreateQuestionDtoToQuestion(CreateQuestionDto createQuestionDto)
    {
        return new Entity.Question
        {
            Id = Guid.NewGuid(),
            Name = createQuestionDto.Name,
            QuestionText = createQuestionDto.QuestionText,
            MultipleAnswers = createQuestionDto.MultipleAnswers,
            HasTextInput = createQuestionDto.HasTextInput,
            IsAnswered = createQuestionDto.IsAnswered,
        };
    }

    public void UpdateQuestionDto(Entity.Question question, UpdateQuestionDto updateQuestionDto)
    {
        question.QuizId = updateQuestionDto.QuizId;
        question.Name = updateQuestionDto.Name;
        question.QuestionText = updateQuestionDto.QuestionText;
        question.MultipleAnswers = updateQuestionDto.MultipleAnswers;
        question.HasTextInput = updateQuestionDto.HasTextInput;
        question.IsAnswered = updateQuestionDto.IsAnswered;
    }
}