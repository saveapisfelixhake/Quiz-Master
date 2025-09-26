using Backend.Domains.Quiz.Dto.Answer;
using Backend.Domains.Quiz.Mapping.Answer;
using Backend.Domains.Quiz.Persistence.Sql.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Quiz.Backend.Controllers;

[ApiController]
[Route("answer")]
public class AnswerController(IDbContextFactory<DataContext> factory, IAnswerMapper mapper) : ControllerBase
{
    [HttpPost("create")]
    public IActionResult CreateAnswer(CreateAnswerDto dto)
    {
        using var context = factory.CreateDbContext();

        var answer = mapper.MapCreateAnswerDtoToAnswer(dto);
        
        context.Answers.Add(answer);
        context.SaveChanges();
        
        return Ok($"Created answer with id {answer.Id}.");
    }

    [HttpPut("update")]
    public IActionResult UpdateAnswer(Guid answerId, UpdateAnswerDto dto)
    {
        using var context = factory.CreateDbContext();

        var answer = context.Answers.Single(x => x.Id == answerId);
        
        mapper.UpdateAnswerDtoToAnswer(dto, answer);
        context.SaveChanges();
        
        return Ok($"Updated answer with id {answerId}.");
    }
    
    [HttpDelete("get")]
    public IActionResult GetAnswers(Guid answerId)
    {
        using var context = factory.CreateDbContext();
        
        var answer = context.Answers.Single(x => x.Id == answerId);

        var dto = mapper.AnswertoGetAnswerDto(answer);
        
        return Ok(dto);
    }
    
    [HttpDelete("all")]
    public IActionResult GetAllAnswers()
    {
        using var context = factory.CreateDbContext();

        var dtoList = context.Answers.ToList().Select(mapper.AnswertoGetAnswerDto).ToList();

        return Ok(dtoList);
    }

    [HttpDelete("delete")]
    public IActionResult DeleteAnswer(Guid answerId)
    {
        using var context = factory.CreateDbContext();
        
        context.Answers.Remove(context.Answers.Single(x => x.Id == answerId));
        context.SaveChanges();
        
        return Ok($"Deleted answer with id {answerId}.");
    }
}