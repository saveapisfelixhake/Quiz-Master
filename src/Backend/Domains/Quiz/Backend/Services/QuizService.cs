using Backend.Domains.Quiz.Dto.Quiz;
using Backend.Domains.Quiz.Mapping.Quiz;
using Backend.Domains.Quiz.Persistence.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Quiz.Backend.Services;

public class QuizService(IDbContextFactory<DataContext> factory, IQuizMapper mapper) : IQuizService
{
    public Guid Create(CreateQuizDto dto)
    {
        using var context = factory.CreateDbContext();
        
        var quiz = mapper.MapCreateQuizDtoToQuiz(dto);
        context.Add(quiz);
        context.SaveChanges();
        
        return quiz.Id;
    }

    public Guid? Update(Guid quizId, UpdateQuizDto dto)
    {
        using var context = factory.CreateDbContext();
        
        var quiz = context.Quizzes.FirstOrDefault(x => x.Id == quizId);
        
        if (quiz == null)
        {
            return null;
        }
        
        mapper.MapUpdateQuizDtoToQuiz(dto, quiz);
        
        context.SaveChanges();
        
        return quiz.Id;
    }

    public GetQuizDto? Get(Guid quizId)
    {
        using var context = factory.CreateDbContext();

        var quiz = context.Quizzes.Include(quiz => quiz.Questions).FirstOrDefault(x => x.Id == quizId);
        return quiz == null ? null : mapper.MapQuizToGetQuizDto(quiz);
    }

    public List<GetQuizDto>? All()
    {
        using var context = factory.CreateDbContext();
        
        var dtoList = context.Quizzes.Select(quiz => mapper.MapQuizToGetQuizDto(quiz)).ToList();
        return dtoList.Count == 0 ? null : dtoList;
    }

    public bool Delete(Guid quizId)
    {
        using var context = factory.CreateDbContext();

        var quiz = context.Quizzes.FirstOrDefault(x => x.Id == quizId);
        
        if (quiz == null)
        {
            return false;
        }
        
        context.Quizzes.Remove(quiz);
        return true;
    }
}