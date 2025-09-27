using Backend.Domains.Quiz.Backend.Services;
using Backend.Domains.Quiz.Dto.Quiz;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Domains.Quiz.Backend.Controllers;

[ApiController]
[Route("quiz")]
public class QuizController(IQuizService service) : ControllerBase
{
    [HttpPost("create")]
    public IActionResult CreateQuiz(CreateQuizDto dto)
    {
        var quizId = service.Create(dto);
        
        return Ok(quizId);
    }

    [HttpPut("update")]
    public IActionResult UpdateQuiz(Guid quizId, UpdateQuizDto dto)
    {
        var guid = service.Update(quizId, dto);
        if (guid == null)
        {
            return NotFound();
        }
        
        return Ok(guid);
    }
    
    [HttpGet("´get")]
    public IActionResult GetQuiz(Guid quizId)
    {
        var dto = service.Get(quizId);
        if (dto == null)
        {
            return NotFound();
        }
        
        return Ok(dto);
    }
    
    
    
    [HttpGet("all")]
    public IActionResult GetAllQuizzes()
    {
        var dtoList = service.All();
        if (dtoList == null)
        {
            return NotFound();
        }

        return Ok(dtoList);
    }

    [HttpDelete("delete")]
    public IActionResult DeleteQuiz(Guid quizId)
    {
        var request = service.Delete(quizId);
        if (!request)
        {
            return NotFound();
        }
        
        return Ok(request);
    }
}