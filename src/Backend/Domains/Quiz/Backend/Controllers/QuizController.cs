using Backend.Domains.Quiz.Dto.Quiz;
using Backend.Domains.Quiz.Mapping.Quiz;
using Backend.Domains.Quiz.Persistence.Sql.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Quiz.Backend.Controllers;

[ApiController]
[Route("quiz")]
public class QuizController(IDbContextFactory<DataContext> factory, IQuizMapper mapper) : ControllerBase
{
    [HttpPost("create")]
    public IActionResult CreateQuiz(CreateQuizDto dto)
    {
        using var context = factory.CreateDbContext();
        
        var quiz = mapper.MapCreateQuizDtoToQuiz(dto);
        context.Add(quiz);
        
        return Ok($"Created quiz with id {quiz.Id}.");
    }

    [HttpPut("update")]
    public IActionResult UpdateQuiz(Guid quizId, UpdateQuizDto dto)
    {
        using var context = factory.CreateDbContext();
        
        var quiz = context.Quizzes.FirstOrDefault(x => x.Id == quizId);
        
        if (quiz == null)
        {
            return NotFound();
        }
        
        mapper.MapUpdateQuizDtoToQuiz(dto, quiz);
        
        context.SaveChanges();
        
        return Ok($"Updated quiz with id {quiz.Id}.");
    }
    
    [HttpDelete("´get")]
    public IActionResult GetQuiz(Guid quizId)
    {
        using var context = factory.CreateDbContext();

        var quiz = context.Quizzes.Include(quiz => quiz.Questions).FirstOrDefault(x => x.Id == quizId);
        if (quiz == null)
        {
            return NotFound();
        }

        var dto = mapper.MapQuizToGetQuizDto(quiz);
        return Ok(dto);
    }
    
    [HttpDelete("all")]
    public IActionResult GetAllQuizzes()
    {
        using var context = factory.CreateDbContext();
        
        var dtoList = context.Quizzes.Select(quiz => mapper.MapQuizToGetQuizDto(quiz)).ToList();

        return Ok(dtoList);
    }

    [HttpDelete("delete")]
    public IActionResult DeleteQuiz(Guid quizId)
    {
        using var context = factory.CreateDbContext();

        var quiz = context.Quizzes.FirstOrDefault(x => x.Id == quizId);
        
        if (quiz == null)
        {
            return NotFound();
        }
        
        context.Quizzes.Remove(quiz);
        
        return Ok($"Deleted quiz with id {quizId}.");
    }
}