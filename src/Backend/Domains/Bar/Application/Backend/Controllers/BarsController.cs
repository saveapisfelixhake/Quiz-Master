using Backend.Domains.Bar.Domain.Models.Dtos.Bar;
using Backend.Domains.Bar.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Domains.Bar.Application.Backend.Controllers;

[ApiController]
[Route("bars")]
public class BarsController(IBarService service) : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<List<BarGetDto>>> GetAll()
    {
        return await service.GetAllAsync().ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<ActionResult<BarGetDto>> Get([FromQuery] Guid id)
    {
        var bar = await service.GetByIdAsync(id).ConfigureAwait(false);
        if (bar == null)
        {
            return NotFound();
        }
        
        return bar;
    }

    [HttpPost("create")]
    public async Task<ActionResult<BarGetDto>> Create(BarCreateDto dto)
    {
        var barId = await service.CreateAsync(dto).ConfigureAwait(false);
        if (barId == null)
        {
            return Conflict();
        }
        
        var bar =  await service.GetByIdAsync(barId.Value).ConfigureAwait(false);
        if (bar == null)
        {
            return BadRequest();
        }

        return bar;
    }

    [HttpPut("update")]
    public async Task<ActionResult<BarGetDto>> Update([FromQuery] Guid id, BarUpdateDto dto)
    {
        var barId  = await service.UpdateAsync(id, dto).ConfigureAwait(false);
        if (barId == null)
        {
            return NotFound();
        }

        var bar = await service.GetByIdAsync(barId.Value).ConfigureAwait(false);
        if (bar == null)
        {
            return NotFound();
        }

        return bar;
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        await  service.DeleteAsync(id).ConfigureAwait(false);

        return NoContent();
    }
}