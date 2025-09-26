using Backend.Domains.Common.Infrastructure.Services;
using Backend.Domains.Quiz.Persistence.Sql.Context;
using Backend.Domains.User.Application.Mapping;
using Backend.Domains.User.Domain.Models.Dto;
using Backend.Domains.User.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domains.User.Application.Backend.Controllers;

[ApiController]
[Route("users")]
public class UsersController(IDbContextFactory<DataContext> factory, IUserMapper mapper, IHashingService hashingService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<List<UserGetDto>>> GetAll()
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var users = await context.Users.ToListAsync().ConfigureAwait(false);

        return users.Select(mapper.MapUserToGetDto).ToList();
    }

    [HttpGet]
    public async Task<ActionResult<UserGetDto>> GetById([FromQuery] Guid id)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var user = await context.Users.SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        if (user == null)
        {
            return NotFound();
        }

        return mapper.MapUserToGetDto(user);
    }

    [HttpPost("create")]
    public async Task<ActionResult<UserGetDto>> Create(UserCreateDto dto)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var existingUser = await context.Users.SingleOrDefaultAsync(x => x.UserName == dto.UserName).ConfigureAwait(false);
        if (existingUser != null)
        {
            return Conflict($"User with username '{dto.UserName}' already exists.");
        }

        var hashedPassword = hashingService.Hash(dto.Password);
        var user = UserEntity.Create(dto.FirstName, dto.LastName, dto.Email, dto.UserName, hashedPassword, dto.IsActive);
        await context.Users.AddAsync(user).ConfigureAwait(false);

        await context.SaveChangesAsync().ConfigureAwait(false);

        return CreatedAtAction(nameof(GetById), new
        {
            id = user.Id
        }, mapper.MapUserToGetDto(user));
    }

    [HttpPut("update")]
    public async Task<ActionResult<UserGetDto>> Update([FromQuery] Guid id, UserUpdateDto dto)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var existingUser = await context.Users.SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        if (existingUser == null)
        {
            return NotFound($"User with id '{id}' not found.");
        }

        existingUser.Update(dto.FirstName, dto.LastName, dto.Email, dto.UserName, dto.IsActive);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return mapper.MapUserToGetDto(existingUser);
    }

    [HttpPatch("update/password")]
    public async Task<IActionResult> UpdatePassword([FromQuery] Guid id, UserUpdatePasswordDto dto)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var existingUser = await context.Users.SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        if (existingUser == null)
        {
            return NotFound($"User with id '{id}' not found.");
        }

        var hashedPassword = hashingService.Hash(dto.Password);
        existingUser.UpdateHash(hashedPassword);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return NoContent();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        await using var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        var existingUser = await context.Users.SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        if (existingUser == null)
        {
            return NotFound($"User with id '{id}' not found.");
        }

        if (existingUser.IsInitialUser)
        {
            return BadRequest("The initial user cannot be deleted.");
        }

        context.Users.Remove(existingUser);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return NoContent();
    }
}
