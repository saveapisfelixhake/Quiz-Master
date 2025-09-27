using Backend.Domains.Quiz.Dto.Question;
using Backend.Domains.Quiz.Mapping.Question;
using Backend.Domains.Quiz.Persistence.Sql.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Quiz.Backend.Controllers;

[ApiController]
[Route("question")]
public class QuestionController(IDbContextFactory<DataContext> factory, IQuestionMapper mapper) : ControllerBase
{
    [HttpPost("create")]
        public IActionResult CreateQuestionForQuiz(Guid quizId, CreateQuestionDto dto)
        {
            using var context = factory.CreateDbContext();
            
            var quiz = context.Quizzes.FirstOrDefault(x => x.Id == quizId);

            if (quiz == null)
            {
                return NotFound();
            }
            
            quiz.Questions.Add(mapper.CreateQuestionDtoToQuestion(dto));
            
            return Ok($"Create question for quiz {quizId}.");
        }
        
    [HttpPut("update")]
    public IActionResult UpdateQuestion(Guid questionId, UpdateQuestionDto dto)
    {
        using var context = factory.CreateDbContext();
        
        var question = context.Questions.SingleOrDefault(x => x.Id == questionId);
        if (question == null)
        {
            return NotFound();
        }
        
        mapper.UpdateQuestionDto(question, dto);
        
        context.SaveChanges();
        
        return Ok($"Updated question with id {question.Id}.");
    }
    
    [HttpGet("get")]
    public IActionResult GetQuestion(Guid questionId)
    {
        using var context = factory.CreateDbContext();
        
        var question = context.Questions.SingleOrDefault(x => x.Id == questionId);
        if (question == null)
        {
            return NotFound();
        }

        var dto = mapper.GetQuestionDto(question);
        
        return Ok(dto);
    }
    
    [HttpGet("all")]
    public IActionResult GetAllQuestion()
    {
        using var context = factory.CreateDbContext();

        var dtoList = context.Quizzes.Select(quiz => quiz.Questions.SingleOrDefault(x => x.Id == quiz.Id))
            .Select(question => mapper.GetQuestionDto(question))
            .ToList();

        return Ok(dtoList);
    }
    
    [HttpDelete("delete")]
    public IActionResult DeleteQuestion(Guid questionId)
    {
        using var context = factory.CreateDbContext();
        
        var question = context.Questions.SingleOrDefault(x => x.Id == questionId);
        if (question == null)
        {
            return NotFound();
        }
        
        context.Questions.Remove(question);
        
        return Ok($"Deleted question with id {questionId}.");
    }
}