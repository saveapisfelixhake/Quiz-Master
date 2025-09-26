using Backend.Domains.Bar.Application.Mapping;
using Backend.Domains.Bar.Domain.Models.Dtos.Bar;
using Backend.Domains.Bar.Domain.Models.Entities;
using Backend.Domains.Quiz.Persistence.Sql.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.Bar.Application.Backend.Controllers;

[ApiController]
[Route("bars")]
public class BarsController(IDbContextFactory<DataContext> factory, IBarMapper mapper) : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<List<BarGetDto>>> GetAll()
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);

        var bars = await context.Bars
            .Include(x => x.Address)
            .ToListAsync()
            .ConfigureAwait(false);
        var dtos = bars.Select(mapper.MapBarToGetDto).ToList();

        return dtos;
    }

    [HttpGet]
    public async Task<ActionResult<BarGetDto>> Get([FromQuery] Guid id)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);

        var bar = await context.Bars
            .Include(x => x.Address)
            .SingleOrDefaultAsync(x => x.Id == id);
        if (bar == null)
        {
            return NotFound($"Bar with id '{id}' not found");
        }
        
        var dto  = mapper.MapBarToGetDto(bar);

        return dto;
    }

    [HttpPost("create")]
    public async Task<ActionResult<BarGetDto>> Create(BarCreateDto dto)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var existingBar = await context.Bars
            .SingleOrDefaultAsync(x => x.Name == dto.Name)
            .ConfigureAwait(false);
        if (existingBar != null)
        {
            return Conflict($"Bar with name '{dto.Name}' already exists");
        }

        var address = await context.BarAddresses
            .SingleOrDefaultAsync(x => x.Street == dto.Address.Street && x.Number == dto.Address.Number)
            .ConfigureAwait(false) ?? BarAddressEntity.Create(dto.Address.Street, dto.Address.Number);
        var bar = BarEntity.Create(dto.Name, dto.IsActive, address.Id);
        context.BarAddresses.Add(address);
        context.Bars.Add(bar);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return CreatedAtAction(nameof(Get), new { id = bar.Id }, mapper.MapBarToGetDto(bar));
    }

    [HttpPut("update")]
    public async Task<ActionResult<BarGetDto>> Update([FromQuery] Guid id, BarUpdateDto dto)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var existingBar = await context.Bars
            .Include(x => x.Address)
            .SingleOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);
        if (existingBar == null)
        {
            return NotFound($"Bar with id '{id}' not found");
        }

        if (!existingBar.HasChanges(dto.Name, dto.Address.Street, dto.Address.Number))
        {
            return Ok(mapper.MapBarToGetDto(existingBar));
        }
        
        existingBar.Update(dto.Name, dto.IsActive, dto.Address.Street, dto.Address.Number);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return Ok(mapper.MapBarToGetDto(existingBar));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var existingBar = await context.Bars
            .Include(x => x.Address)
            .SingleOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);
        if (existingBar == null)
        {
            return NotFound($"Bar with id '{id}' not found");
        }

        context.Bars.Remove(existingBar);
        await  context.SaveChangesAsync().ConfigureAwait(false);

        return NoContent();
    }
}